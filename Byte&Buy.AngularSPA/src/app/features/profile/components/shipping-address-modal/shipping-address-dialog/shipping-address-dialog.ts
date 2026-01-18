import { ChangeDetectorRef, Component, effect, EventEmitter, inject, Input, input, Output, signal } from '@angular/core';
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
import { HttpErrorResponse } from '@angular/common/http';

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
  private snackbarService = inject(ToastService);

  shippingForm!: FormGroup;
  loading = signal<boolean>(false);
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
      houseNumber: new FormControl(data?.houseNumber || '', Validators.required),
      postalCity: new FormControl(data?.postalCity || '', Validators.required),
      postalCode: new FormControl(data?.postalCode || '', [Validators.required]),
      city: new FormControl(data?.city || '', Validators.required),
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

    this.loading.set(true);
    const payload = this.buildPayload();

    const request$: Observable<unknown> = this.addressId
      ? this.addressApiService.putShippingAddress(this.addressId, payload as ShippingAddressUpdateRequest)
      : this.addressApiService.postShippingAddress(payload as ShippingAddressAddRequest);


    request$
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: () => {
          this.snackbarService.success(`Shipping address ${this.isEditMode ? 'updated' : 'created'}!`);
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
    this.loading.set(true);
    this.addressApiService.getShippingAddress(this.addressId!)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (address) => {
          this.initForm(address);
        },
        error: (err: ProblemDetails) => {
          this.snackbarService.error(err.detail ?? "Failed to load address");
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
    this.loading.set(false);
    this.error.set(null);
  }
}
