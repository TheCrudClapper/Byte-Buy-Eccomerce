import { Component, inject, OnInit, signal } from '@angular/core';
import { AddressApiService } from '../../../../core/clients/address/address-api-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { getErrorMessage } from '../../../../shared/helpers/form-helper';
import { finalize } from 'rxjs';
import { HomeAddressDto } from '../../../../core/dto/home-address/home-address-dto';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { SelectListItem } from '../../../../shared/models/select-list-item';
import { CountryApiService } from '../../../../core/clients/country/country-api-service';
import { ShippingAddressListItem } from '../../model/shipping-address-list-item';
import { Guid } from 'guid-typescript';
import { ShippingAddressDialog } from "../shipping-address-modal/shipping-address-dialog/shipping-address-dialog";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-addresses',
  imports: [CommonModule, ReactiveFormsModule, ShippingAddressDialog],
  templateUrl: './addresses.html',
  styleUrl: './addresses.scss',
  standalone: true
})
export class Addresses implements OnInit {
  private readonly addressApiService: AddressApiService = inject(AddressApiService);
  private readonly countriesApiService: CountryApiService = inject(CountryApiService);
  private readonly snackbarService: ToastService = inject(ToastService);

  isLoading = signal<boolean>(false);
  countriesList = signal<SelectListItem[]>([]);
  shippingAddresses = signal<ShippingAddressListItem[]>([]);
  displayShippingDialog = signal<boolean>(false);
  selectedShippingId: Guid | null = null;

  homeAddressForm: FormGroup = new FormGroup({
    street: new FormControl<string>("", Validators.required),
    houseNumber: new FormControl("", Validators.required),
    postalCity: new FormControl("", Validators.required),
    postalCode: new FormControl("", Validators.required),
    city: new FormControl("", Validators.required),
    country: new FormControl("", Validators.required),
    flatNumber: new FormControl(""),
  });

  ngOnInit(): void {
    this.loadHomeAddress();
    this.loadShippingAddresses();
    this.loadCountries();
  }

  onHomeAddressSubmit() {
    if (this.homeAddressForm.invalid) {
      this.homeAddressForm.markAllAsTouched();
      return;
    }
    var data = this.homeAddressForm.value;
    const payload: HomeAddressDto = {
      city: data.city,
      houseNumber: data.houseNumber,
      flatNumber: data.flatNumber,
      postalCity: data.postalCity,
      postalCode: data.postalCode,
      street: data.street,
      country: data.country
    };

    this.isLoading.set(true);
    this.addressApiService.putHomeAddress(payload)
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: () => {
          this.snackbarService.success("Successfully saved changes");
        },
        error: () => {
          this.snackbarService.success("Something went wrong");
        }
      });
  }

  getHomeError(path: string) {
    return getErrorMessage(this.homeAddressForm, path);
  }

  loadHomeAddress() {
    this.isLoading.set(true);

    this.addressApiService.getHomeAddress()
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe(response => {
        this.homeAddressForm.patchValue({
          street: response.street,
          houseNumber: response.houseNumber,
          postalCity: response.postalCity,
          postalCode: response.postalCode,
          city: response.postalCity,
          country: response.country,
          flatNumber: response.flatNumber
        });
      })
  }

  loadShippingAddresses() {
    this.addressApiService.getShippingAddressesList()
      .subscribe({
        next: data => { this.shippingAddresses.set(data) },
        error: () => this.snackbarService.error('Failed to load shipping addresses')
      });
  }

  loadCountries(){
    this.countriesApiService.getSelectList()
    .subscribe({
      next: data => { this.countriesList.set(data) }
    });
  }
  
  showShippingDialog(id: Guid | null) {
    this.selectedShippingId = id;
    this.displayShippingDialog.set(true);
  }

  closeShippingDialog() {
    this.displayShippingDialog.set(false);
    this.selectedShippingId = null;
  }

  onShippingSaved() {
    this.loadShippingAddresses();
    this.closeShippingDialog();
  }

  deleteShippingAddress(id: Guid) {
    if (confirm("Are you sure you want to delete this address ?")) {
      this.addressApiService.deleteShippingAddress(id)
        .subscribe({
          next: () => {
            const currentAddresses = this.shippingAddresses();
            this.shippingAddresses.set(
            currentAddresses.filter(address => address.id !== id));
          },
          error: () => {
            alert("Failed to delete address");
          }
        });
    }
  }
}
