import { Component, inject, OnInit, signal } from '@angular/core';
import { Guid } from 'guid-typescript';
import { FormArray, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BaseOfferForm, OfferMode, OfferType } from '../../../shared/components/base-offer-form/base-offer-form';

@Component({
  selector: 'app-sale-create',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './sale-create.html',
  styleUrls: [
    './sale-create.scss',
    '../../../shared/styles/offers-shared-styles.scss']
})
export class SaleCreate extends BaseOfferForm {
  override type: OfferType = 'sale';
  override mode: OfferMode = 'add';

  form: FormGroup = new FormGroup({
    name: new FormControl("", [Validators.required, Validators.maxLength(75), Validators.minLength(16)]),
    selectedCategoryId: new FormControl<string | null>(null, [Validators.required]),
    selectedConditionId: new FormControl<string | null>(null, [Validators.required]),
    pricePerItem: new FormControl<number>(1, [Validators.required, Validators.min(1)]),
    quantityAvailable: new FormControl<number>(1, [Validators.required, Validators.min(1)]),
    description: new FormControl<string>("", [Validators.maxLength(2000), Validators.required]),
    parcelLockerDeliveries: new FormArray<FormControl<Guid | null>>([]),
    otherDeliveriesIds: new FormArray<FormControl<Guid>>([], [Validators.required, Validators.minLength(1)]),
  });


  override buildFormData(): FormData {
    const form = this.form.value;
    const fd = new FormData();

    fd.append('CategoryId', form.selectedCategoryId!);
    fd.append('ConditionId', form.selectedConditionId!);
    fd.append('Name', form.name);
    fd.append('Description', form.description);
    fd.append('QuantityAvailable', String(form.quantityAvailable));
    fd.append('PricePerItem', String(form.pricePerItem));

    this.images().forEach((img, i) => {
      fd.append(`Images[${i}].Image`, img.file!);
      if (img.alt) fd.append(`Images[${i}].AltText`, img.alt);
    });

    form.otherDeliveriesIds.forEach((id: Guid, index: number) => {
      fd.append(`OtherDeliveriesIds[${index}]`, String(id));
    });

    return fd;
  }
}


