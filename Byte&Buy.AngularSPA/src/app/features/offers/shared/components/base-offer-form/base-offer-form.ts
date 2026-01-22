import { Component, inject, OnInit, signal } from '@angular/core';
import { OffersFacade } from '../../../services/offers-facade';
import { ImageItem } from '../../../models/image-item';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { ActivatedRoute, Router } from '@angular/router';

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
  private readonly router = inject(Router);

  abstract readonly type: OfferType;
  abstract readonly mode: OfferMode;
  abstract form: FormGroup;
  images = signal<ImageItem[]>([]);

  protected abstract buildFormData(): FormData;

  ngOnInit(): void {
    this.facade.init();
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

  getErrorMessage(path: string) {
    return getErrorMessage(this.form, path);
  }
}
