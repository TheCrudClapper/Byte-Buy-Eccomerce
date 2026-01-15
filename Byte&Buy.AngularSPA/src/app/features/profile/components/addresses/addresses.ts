import { Component, inject, OnInit, signal } from '@angular/core';
import { AddressApiService } from '../../../../core/services/address/address-api-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { getErrorMessage } from '../../../../core/helpers/form-helper';
import { finalize, Observable } from 'rxjs';
import { HomeAddressDto } from '../../../../shared/api-dto/home-address-dto';
import { SnackbarService } from '../../../../core/services/snackbar/snackbar-service';
import { SelectListItem } from '../../../../shared/models/select-list-item';
import { CountryApiService } from '../../../../core/services/country/country-api-service';
import { ShippingAddressListItem } from '../../model/shipping-address-list-item';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-addresses',
  imports: [ReactiveFormsModule],
  templateUrl: './addresses.html',
  styleUrl: './addresses.scss',
})
export class Addresses implements OnInit {
  private readonly addressApiService: AddressApiService = inject(AddressApiService);
  private readonly countriesApiService: CountryApiService = inject(CountryApiService);
  private readonly snackbarService: SnackbarService = inject(SnackbarService);

  isLoading = signal<boolean>(false);
  countriesList$!: Observable<SelectListItem[]>;
  shippingAddresses = signal<ShippingAddressListItem[]>([]);

  homeAddressForm: FormGroup = new FormGroup({
    street: new FormControl<string>("", Validators.required),
    houseNumber: new FormControl("", Validators.required),
    postalCity: new FormControl("", Validators.required),
    postalCode: new FormControl("", Validators.required),
    city: new FormControl("", Validators.required),
    country: new FormControl("", Validators.required),
    flatNumber: new FormControl(""),
  });

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

  ngOnInit(): void {
    this.loadHomeAddress();
    this.loadShippingAddresses();
    console.log(this.shippingAddresses());
    this.countriesList$ = this.countriesApiService.getSelectList();
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

}
