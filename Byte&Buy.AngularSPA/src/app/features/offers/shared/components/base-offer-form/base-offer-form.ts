import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { OffersFacade } from '../../../services/offers-facade';
import { ImageItem } from '../../../models/image-item';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../../../environments/environment';


export type OfferMode = 'add' | 'edit';
export type OfferType = 'sale' | 'rent';

//Base Component class for sale -> create/edit and rent -> create/edit 
//This class encapsulates common UI logic shared throught mentioned components

@Component({
  selector: 'app-base-offer-form',
  imports: [],
  template: ``,
  styleUrl: './base-offer-form.scss',
})
export abstract class BaseOfferForm implements OnInit {
  protected readonly facade = inject(OffersFacade);
  protected readonly route = inject(ActivatedRoute);

  abstract readonly type: OfferType;
  abstract readonly mode: OfferMode;
  abstract form: FormGroup;
  images = signal<ImageItem[]>([]);

  protected abstract buildFormData(): FormData;

  private readonly parcelEffect = effect(() => {
    const parcels = this.facade.deliveries().parcel;
    if (parcels.length === 0) return;

    this.initParcelControls();
  });

  ngOnInit(): void {
    this.facade.init();

    if(this.mode == 'add')
      this.facade.getHomeAddressForOffer();
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload = this.buildFormData();
    this.facade.submit(
      this.type,
      this.mode,
      payload,
      this.images(),
      this.getOfferId()
    )
  }

  //needs to be overriden by edit components
  protected getOfferId(): Guid | undefined {
    return undefined;
  }

  //initializes parcel lockers radio buttons
  protected initParcelControls(): void {
    const parcelGroup = this.form.get('parcelLockerDeliveries') as FormGroup;

    if (parcelGroup === null)
      throw Error("Form needs to have parcelLockerDeliveries field");

    this.facade.deliveries().parcel.forEach(group => {
      if (!parcelGroup.contains(group.carrier)) {
        parcelGroup.addControl(
          group.carrier,
          new FormControl<Guid | null>(null)
        );
      }
    });
  }

  //extracts parcel lockers from form
  protected getSelectedParcelLockers(): Guid[] {
    const values = this.form.value.parcelLockerDeliveries ?? [];
    return Object.values(values)
      .filter((v): v is Guid => !!v);
  }

  protected restoreSelectedParcelLockers(): void {
    if (this.mode !== 'edit')
      return;

    const offer = this.facade.currentOffer();
    if (!offer) return;

    const selectedIds = offer.data.parcelLockerDeliveries;

    const parcelGroup = this.form.get('parcelLockerDeliveries') as FormGroup;
    const parcelGroups = this.facade.deliveries().parcel;

    for (const group of parcelGroups) {
      const match = group.options.find(o =>
        selectedIds?.includes(o.id)
      );

      if (match) {
        parcelGroup.get(group.carrier)?.setValue(match.id);
      }
    }
  }

  onDeliveryToggle(id: Guid, checked: boolean): void {
    const array = this.form.get('otherDeliveriesIds') as FormArray
    if (checked)
      array.push(new FormControl(id));
    else {
      const index = array.controls.findIndex(c => c.value === id);
      if (index !== -1) {
        array.removeAt(index);
      }
    }
    array.updateValueAndValidity();
  }

  onImagesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files) return;

    const files = Array.from(input.files);

    for (const file of files) {
      if (this.images.length >= 10) break;
      if (!file.type.startsWith('image/')) continue;
      if (file.size > 5 * 1024 * 1024) continue;

      const reader = new FileReader();
      reader.onload = () => {
        this.images.update(imgs => [
          ...imgs,
          {
            file,
            isNew: true,
            preview: reader.result as string,
            alt: ''
          }
        ]);
      };
      reader.readAsDataURL(file);
    }
    input.value = '';
  }

  removeImage(index: number): void {
    this.images.update(imgs => imgs.filter((_, i) => i !== index));
  }

  //for edit and already existing images only
  revertDelete(index: number): void {
    this.images.update(imgs => {
      const img = imgs[index];
      if (img.isDeleted)
        img.isDeleted = false;
      return [...imgs];
    });
  }

  getImagePath(path: string | undefined) {
    if (!path) return '';
    return environment.staticImagesBaseUrl + path;
  }

  getErrorMessage(path: string) {
    return getErrorMessage(this.form, path);
  }

}
