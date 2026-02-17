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
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { EmptyStateModel } from '../../../../shared/models/empty-state-model';
import { EmptyState } from "../../../../shared/components/empty-state/empty-state";
import { DialogService } from '../../../../shared/services/dialog-service/dialog-service';

@Component({
  selector: 'app-addresses',
  imports: [CommonModule, ReactiveFormsModule, ShippingAddressDialog, EmptyState],
  templateUrl: './addresses.html',
  styleUrl: './addresses.scss',
  standalone: true
})
export class Addresses implements OnInit {
  private readonly addressApiService = inject(AddressApiService);
  private readonly countriesApiService = inject(CountryApiService);
  private readonly dialogService = inject(DialogService);
  private readonly toastService = inject(ToastService);

  readonly emptyStateModel: EmptyStateModel = {
    description: `You haven't defined any shipping address.
       To use courier delivery, you need to add one.`,
    header: "No shipping addresses",
    mainIconClass: "fa-solid fa-location-dot",
    backgroundClass: 'var(--background)'
  };

  isLoading = signal<boolean>(false);
  countriesList = signal<SelectListItem[]>([]);
  shippingAddresses = signal<ShippingAddressListItem[]>([]);
  displayShippingDialog = signal<boolean>(false);
  selectedShippingId: Guid | null = null;

  homeAddressForm: FormGroup = new FormGroup({
    street: new FormControl<string>("", [Validators.required, Validators.maxLength(50)]),
    houseNumber: new FormControl("", [Validators.required, Validators.maxLength(10)]),
    postalCity: new FormControl("", [Validators.required, Validators.maxLength(50)]),
    postalCode: new FormControl("", [Validators.required, Validators.maxLength(20)]),
    city: new FormControl("", [Validators.required, Validators.maxLength(50)]),
    country: new FormControl("", [Validators.required, Validators.maxLength(50)]),
    flatNumber: new FormControl("", Validators.maxLength(10)),
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
          this.toastService.success("Successfully saved changes");
        },
        error: () => {
          this.toastService.success("Something went wrong");
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
        error: () => this.toastService.error('Failed to load shipping addresses')
      });
  }

  loadCountries() {
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
    this.dialogService.confirm({ title: "Are you sure you want to delete this address ?" })
      .then(result => {
        if (result.isConfirmed) {
          this.addressApiService.deleteShippingAddress(id)
            .subscribe({
              next: () => {
                const currentAddresses = this.shippingAddresses();
                this.shippingAddresses.set(
                  currentAddresses.filter(address => address.id !== id));
                this.dialogService.success("Successfully deleted shipping address.");
              },
              error: (err: ProblemDetails) => {
                this.dialogService.error(err.detail ?? "Failed to delete shipping address.");
              }
            });
        }
      })
  }
}
