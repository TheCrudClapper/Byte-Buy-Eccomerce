import { Component, effect, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { finalize, Observable } from 'rxjs';
import { AddressApiService } from '../../../../../core/clients/address/address-api-service';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { CommonModule } from '@angular/common';
import { ShippingAddressResponse } from '../../../../../core/dto/shipping-address/shipping-address-response';
import { Guid } from 'guid-typescript';
import { ShippingAddressUpdateRequest } from '../../../../../core/dto/shipping-address/shipping-address-update-request';
import { ShippingAddressAddRequest } from '../../../../../core/dto/shipping-address/shipping-address-add-request';
import { SelectListItem } from '../../../../../shared/models/select-list-item';
import { ProblemDetails } from '../../../../../core/dto/problem-details';

@Component({
  selector: 'app-shipping-address-dialog',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  standalone: true,
  templateUrl: './shipping-address-dialog.html',
  styleUrl: './shipping-address-dialog.scss',
})
export class ShippingAddressDialog {
  @Input() countries: SelectListItem[] = [];

  @Input() visible = signal<boolean>(false);
  @Output() visibleChange = new EventEmitter<boolean>();

  @Input() addressId: Guid | null = null;
  @Output() onSaved = new EventEmitter<void>();

  private addressApiService = inject(AddressApiService);
  private toastService = inject(ToastService);

  shippingForm!: FormGroup;
  isLoading = signal<boolean>(false);
  isEditMode = false;
  error = signal<string | null>(null);

  constructor() {
    effect(() => {
      if (this.visible()) {
        if (this.addressId) {
          this.isEditMode = true;
          this.loadAddressData();
        } else {
          this.isEditMode = false;
          this.initForm();
        }
      }
    });
  }

  initForm(data?: ShippingAddressResponse) {
    this.shippingForm = new FormGroup({
      label: new FormControl(data?.label || '', [Validators.required, Validators.maxLength(50)]),
      street: new FormControl(data?.street || '', [Validators.required, Validators.maxLength(50)]),
      houseNumber: new FormControl(data?.houseNumber || '', [Validators.required, Validators.maxLength(10)]),
      postalCity: new FormControl(data?.postalCity || '', [Validators.required, Validators.maxLength(50)]),
      postalCode: new FormControl(data?.postalCode || '', [Validators.required, Validators.maxLength(20)]),
      city: new FormControl(data?.city || '', [Validators.required, Validators.maxLength(50)]),
      flatNumber: new FormControl(data?.flatNumber || '', Validators.maxLength(10)),
      selectedCountryId: new FormControl(data?.countryId || null, Validators.required),
      isDefault: new FormControl(data?.isDefault ?? false)
    });
  }

  save() {
    if (this.shippingForm.invalid) {
      this.shippingForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    const payload = this.buildPayload();

    const request$: Observable<unknown> = this.addressId
      ? this.addressApiService.putShippingAddress(this.addressId, payload as ShippingAddressUpdateRequest)
      : this.addressApiService.postShippingAddress(payload as ShippingAddressAddRequest);

    request$
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: () => {
          this.toastService.success(`Shipping address ${this.isEditMode ? 'updated' : 'created'}!`);
          this.close();
          this.onSaved.emit();
        },
        error: (err: ProblemDetails) => {
          this.error.set(err.detail ?? `Failed to ${this.isEditMode ? 'update' : 'create'} address`);
        }
      });
  }

  getError(path: string) {
    return getErrorMessage(this.shippingForm, path);
  }

  loadAddressData() {
    this.isLoading.set(true);
    this.addressApiService.getShippingAddress(this.addressId!)
      .pipe(finalize(() => this.isLoading.set(false)))
      .subscribe({
        next: (address) => {
          this.initForm(address);
        },
        error: (err: ProblemDetails) => {
          this.toastService.error(err.detail ?? "Failed to load address");
          this.close();
        }
      })
  }

  buildPayload(): ShippingAddressUpdateRequest | ShippingAddressAddRequest {
    var data = this.shippingForm.value;

    const basePayload = {
      street: data.street,
      city: data.city,
      flatNumber: data.flatNumber,
      houseNumber: data.houseNumber,
      postalCity: data.postalCity,
      postalCode: data.postalCode,
      label: data.label,
      isDefault: data.isDefault ?? false,
      countryId: data.selectedCountryId,
    }
    return basePayload as any;
  }

  close() {
    this.visible.set(false);
    this.visibleChange.emit(false);
    this.shippingForm?.reset();
    this.isLoading.set(false);
    this.error.set(null);
  }
}
