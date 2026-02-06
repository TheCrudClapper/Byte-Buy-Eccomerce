import { Component } from '@angular/core';
import { Guid } from 'guid-typescript';
import { ReactiveFormsModule, FormGroup, Validators, FormControl, FormArray, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BaseOfferForm, OfferMode, OfferType } from '../../../shared/components/base-offer-form/base-offer-form';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-rent-create',
  imports: [ReactiveFormsModule, CommonModule, FormsModule, RouterLink],
  templateUrl: './rent-create.html',
  standalone: true,
  styleUrls: [
    './rent-create.scss',
    '../../../shared/styles/offers-shared-styles.scss']
})
export class RentCreate extends BaseOfferForm {

  override type: OfferType = 'rent';
  override mode: OfferMode = 'add';

  form = new FormGroup({
    name: new FormControl("", [Validators.required, Validators.maxLength(75), Validators.minLength(16)]),
    selectedCategoryId: new FormControl<string | null>(null, [Validators.required]),
    selectedConditionId: new FormControl<string | null>(null, [Validators.required]),
    pricePerDay: new FormControl<number>(1, [Validators.required, Validators.min(1)]),
    quantityAvailable: new FormControl<number>(1, [Validators.required, Validators.min(1)]),
    maxRentalDays: new FormControl<number>(1, [Validators.required, Validators.min(1), Validators.max(360)]),
    description: new FormControl<string>("", [Validators.maxLength(2000), Validators.required]),
    parcelLockerDeliveries: new FormGroup({}),
    otherDeliveriesIds: new FormArray<FormControl<Guid>>([], [Validators.required, Validators.minLength(1)]),
  });

  override buildFormData(): FormData {
    const form = this.form.value;
    const fd = new FormData();

    fd.append('Name', form.name!);
    fd.append('CategoryId', form.selectedCategoryId!);
    fd.append('ConditionId', form.selectedConditionId!);
    fd.append('Description', form.description!);
    fd.append('PricePerDay', String(form.pricePerDay));
    fd.append('QuantityAvailable', String(form.quantityAvailable));
    fd.append('MaxRentalDays', String(form.maxRentalDays));

    this.images().forEach((img, i) => {
      fd.append(`Images[${i}].Image`, img.file!);
      fd.append(`Images[${i}].AltText`, img.alt ?? '');
    });

    form.otherDeliveriesIds?.forEach((id, i) =>
      fd.append(`OtherDeliveriesIds[${i}]`, String(id))
    );

    const parcelIds = this.getSelectedParcelLockers();
    parcelIds.forEach((id, i) => fd.append(`ParcelLockerDeliveries[${i}]`, String(id)));
    return fd;
  }
}





