import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { CommonModule } from '@angular/common';
import { BaseOfferForm, OfferMode, OfferType } from '../../../shared/components/base-offer-form/base-offer-form';
import { UserRentOfferResponse } from '../../../../../core/dto/offers/rent/user-rent-offer-response';

@Component({
  selector: 'app-rent-edit',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './rent-edit.html',
  styleUrls: ['./rent-edit.scss',
    '../../../shared/styles/offers-shared-styles.scss'],
})

export class RentEdit extends BaseOfferForm implements OnInit {
  
  override type: OfferType = 'rent';
  override mode: OfferMode = 'edit';

  offerId = signal<Guid | undefined>(undefined);

  form: FormGroup = new FormGroup({
    name: new FormControl("", [Validators.required, Validators.maxLength(75), Validators.minLength(16)]),
    selectedCategoryId: new FormControl<string | null>(null, [Validators.required]),
    selectedConditionId: new FormControl<string | null>(null, [Validators.required]),
    pricePerDay: new FormControl<number>(1, [Validators.required, Validators.min(1)]),
    additionalQuantity: new FormControl<number>(0, [Validators.required, Validators.min(0)]),
    additionalRentalDays: new FormControl<number>(0, [Validators.required, Validators.min(0), Validators.max(359)]),
    description: new FormControl<string>("", [Validators.maxLength(2000), Validators.required]),
    parcelLockerDeliveries: new FormGroup({}),
    otherDeliveriesIds: new FormArray<FormControl<Guid>>([], [Validators.required, Validators.minLength(1)]),
  });

  protected override getOfferId(): Guid | undefined {
    return this.offerId() ? this.offerId() : undefined;
  }

  constructor() {
    super();

    effect(() => {
      const offer = this.facade.currentOffer();
      if (!offer || offer.type !== 'rent') return;

      this.patchForm(offer.data);
    });
  }
  
  override ngOnInit(): void {
    super.ngOnInit();

    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    this.offerId.set(Guid.parse(id));
    this.facade.loadOffer(this.type, Guid.parse(id));
  }

  patchForm(offer: UserRentOfferResponse) {
    this.form.patchValue({
      name: offer.name,
      selectedCategoryId: offer.categoryId,
      selectedConditionId: offer.conditionId,
      description: offer.description,
      pricePerItem: offer.pricePerDay.amount
    });

    const otherDeliveries = this.form.get('otherDeliveriesIds') as FormArray;
    otherDeliveries.clear();
    offer.otherDeliveriesIds.forEach(id => otherDeliveries.push(new FormControl(id)));

    this.images.set([
      ...offer.images.map(img => ({
        id: img.id,
        alt: img.altText,
        preview: img.imagePath,
        isNew: false,
        isDeleted: false
      }))
    ]);
  }

  buildFormData(): FormData {
    const form = this.form.value;
    const fd = new FormData();

    fd.append('CategoryId', form.selectedCategoryId!);
    fd.append('ConditionId', form.selectedConditionId!);
    fd.append('Name', form.name);
    fd.append('Description', form.description);
    fd.append('AdditionalQuantity', String(form.additionalQuantity));
    fd.append('PricePerDay', String(form.pricePerDay));
    fd.append('AdditionalRentalDays', String(form.additionalRentalDays));

    this.images().filter(i => i.isNew).forEach((img, index) => {
      fd.append(`NewImages[${index}].Image`, img.file!);
      fd.append(`NewImages[${index}].AltText`, img.alt ?? '');
    });

    this.images().filter(i => !i.isNew).forEach((img, index) => {
      fd.append(`ExistingImages[${index}].Id`, String(img.id!));
      fd.append(`ExistingImages[${index}].AltText`, img.alt ?? '');
      fd.append(`ExistingImages[${index}].IsDeleted`, String(img.isDeleted ?? false));
    });

    form.otherDeliveriesIds.forEach((id: Guid, index: number) => {
      fd.append(`OtherDeliveriesIds[${index}]`, String(id));
    });

    const parcelIds = this.getSelectedParcelLockers();
    parcelIds.forEach((id, i) => fd.append(`ParcelLockerDeliveries[${i}]`, String(id)));
    return fd;

    return fd;
  }

  override removeImage(index: number): void {
    this.images.update(imgs => {
      const img = imgs[index];
      if (img.isNew) {
        imgs.splice(index, 1);
      } else {
        img.isDeleted = true;
      }
      return [...imgs];
    });
  }
}
