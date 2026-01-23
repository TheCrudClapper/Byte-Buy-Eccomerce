import { CommonModule } from '@angular/common';
import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { BaseOfferForm, OfferMode, OfferType } from '../../../shared/components/base-offer-form/base-offer-form';
import { UserSaleOfferResponse } from '../../../../../core/dto/offers/sale/user-sale-offer-response';

@Component({
  selector: 'app-sale-edit',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './sale-edit.html',
  styleUrls: ['./sale-edit.scss',
    '../../../shared/styles/offers-shared-styles.scss']
})
export class SaleEdit extends BaseOfferForm implements OnInit {
  protected override initParcelControls(): void {
    throw new Error('Method not implemented.');
  }
  protected override getSelectedParcelLockers(): Guid[] {
    throw new Error('Method not implemented.');
  }
  override type: OfferType = 'sale';
  override mode: OfferMode = 'edit'

  offerId = signal<Guid | undefined>(undefined);

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

  constructor() {
    super();

    effect(() => {
      const offer = this.facade.currentOffer();
      if (!offer || offer.type !== 'sale') return;

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

  patchForm(offer: UserSaleOfferResponse) {
    this.form.patchValue({
      name: offer.name,
      selectedCategoryId: offer.categoryId,
      selectedConditionId: offer.conditionId,
      description: offer.description,
      pricePerItem: offer.pricePerItem.amount,
      quantityAvailable: offer.quantityAvailable,
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

  protected override getOfferId(): Guid | undefined {
    return this.offerId() ? this.offerId() : undefined;
  }

  override buildFormData(): FormData {
    const form = this.form.value;
    const fd = new FormData();

    fd.append('CategoryId', form.selectedCategoryId!);
    fd.append('ConditionId', form.selectedConditionId!);
    fd.append('Name', form.name);
    fd.append('Description', form.description);
    fd.append('QuantityAvailable', String(form.quantityAvailable));
    fd.append('PricePerItem', String(form.pricePerItem));

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
