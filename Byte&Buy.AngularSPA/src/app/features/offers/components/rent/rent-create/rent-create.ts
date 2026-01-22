import { Component } from '@angular/core';
import { Guid } from 'guid-typescript';
import { ReactiveFormsModule, FormGroup, Validators, FormControl, FormArray, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BaseOfferForm, OfferMode, OfferType } from '../../../shared/components/base-offer-form/base-offer-form';

@Component({
  selector: 'app-rent-create',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
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
    maxRentalDays: new FormControl<number>(1, [Validators.required, Validators.min(1)]),
    description: new FormControl<string>("", [Validators.maxLength(2000), Validators.required]),
    parcelLockerDeliveries: new FormArray<FormControl<Guid | null>>([]),
    otherDeliveriesIds: new FormArray<FormControl<Guid>>([], [Validators.required, Validators.minLength(1)]),
  });


  override buildFormData(): FormData {
    const v = this.form.value;
    const fd = new FormData();

    fd.append('Name', v.name!);
    fd.append('CategoryId', v.selectedCategoryId!);
    fd.append('ConditionId', v.selectedConditionId!);
    fd.append('Description', v.description!);
    fd.append('PricePerDay', String(v.pricePerDay));
    fd.append('QuantityAvailable', String(v.quantityAvailable));
    fd.append('MaxRentalDays', String(v.maxRentalDays));

    this.images().forEach((img, i) => {
      fd.append(`Images[${i}].Image`, img.file!);
      if (img.alt) fd.append(`Images[${i}].AltText`, img.alt);
    });

    v.otherDeliveriesIds?.forEach((id, i) =>
      fd.append(`OtherDeliveriesIds[${i}]`, String(id))
    );

    return fd;
  }
}





