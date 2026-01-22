import { CommonModule } from '@angular/common';
import { Component, effect, inject, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryApiService } from '../../../../../core/clients/category/category-api-service';
import { DeliveryApiService } from '../../../../../core/clients/delivery/delivery-api-service';
import { ConditionApiService } from '../../../../../core/clients/condition/condition-api-service';
import { AddressApiService } from '../../../../../core/clients/address/address-api-service';
import { SaleOfferApiService } from '../../../../../core/clients/offers/sale/sale-offer-api-service';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { SelectListItem } from '../../../../../shared/models/select-list-item';
import { Observable } from 'rxjs';
import { DeliveryListItem } from '../../../../../shared/models/delivery-list-items';
import { HomeAddressDto } from '../../../../../core/dto/home-address/home-address-dto';
import { ImageItem } from '../../../models/image-item';
import { Guid } from 'guid-typescript';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { mapToListItem } from '../../../../../shared/mappers/offer-mappers';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { ActivatedRoute, Router } from '@angular/router';
import { UserSaleOfferResponse } from '../../../../../core/dto/offers/sale/user-sale-offer-response';

@Component({
  selector: 'app-sale-edit',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './sale-edit.html',
  styleUrls: ['./sale-edit.scss',
    '../../../shared/styles/offers-shared-styles.scss']
})
export class SaleEdit {
  protected readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly categoryApiService = inject(CategoryApiService);
  private readonly conditionApiService = inject(ConditionApiService);
  private readonly deliveryApiService = inject(DeliveryApiService);
  private readonly addressApiService = inject(AddressApiService);
  private readonly saleOfferService = inject(SaleOfferApiService);
  private readonly toastService = inject(ToastService);
  protected readonly imageBaseUrl = 'http://localhost:5099/Images/'

  categoriesOptions$!: Observable<SelectListItem[]>;
  conditionOptions$!: Observable<SelectListItem[]>;
  deliveries = signal<{ parcel: DeliveryListItem[], courier: DeliveryListItem[], pickup: DeliveryListItem[] }>({
    parcel: [],
    courier: [],
    pickup: []
  });

  offerId = signal<Guid | null>(null);
  homeAddress = signal<HomeAddressDto | null>(null);
  images = signal<ImageItem[]>([]);

  constructor() {
    effect(() => {
      const id = this.route.snapshot.paramMap.get('id');
      if (!id) return;
      this.offerId.set(Guid.parse(id));
      this.loadSale(Guid.parse(id));
    });

    this.deliveryApiService.getAvaliableDeliveries()
      .subscribe(response => {
        this.deliveries.set({
          parcel: response.parcelLockerDeliveries.map(mapToListItem),
          courier: response.courierDeliveries.map(mapToListItem),
          pickup: response.pickupPointDeliveries.map(mapToListItem)
        })
      });
  }

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


  ngOnInit(): void {
    this.categoriesOptions$ = this.categoryApiService.getSelectList();
    this.conditionOptions$ = this.conditionApiService.getSelectList();

    this.addressApiService.getHomeAddress().subscribe(address => {
      this.homeAddress.set(address);
    });
  }

  loadSale(id: Guid) {
    if (!id) return;

    this.saleOfferService.getById(id).subscribe({
      next: (sale) => {
        this.form.patchValue({
          name: sale.name,
          selectedCategoryId: sale.categoryId,
          selectedConditionId: sale.conditionId,
          description: sale.description,
          pricePerItem: sale.pricePerItem.amount,
          quantityAvailable: sale.quantityAvailable,
        });

        const otherDeliveries = this.form.get('otherDeliveriesIds') as FormArray;
        otherDeliveries.clear();
        sale.otherDeliveriesIds.forEach(id => otherDeliveries.push(new FormControl(id)));

        this.images.set([
          ...sale.images.map(img => ({
            id: img.id,
            alt: img.altText,
            preview: img.imagePath,
            isNew: false,
            isDeleted: false
          }))
        ]);
      },
      error: (err: ProblemDetails) => this.router.navigate(['/not-found'])
    })
  }

  private buildFormData(): FormData {
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

  onSubmit(): void {
    if (this.images().length === 0) {
      this.toastService.error("Add at least one image");
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    };

    if (!this.homeAddress()) {
      this.toastService.success("To add offer you need to add address !");
      return;
    }

    const payload = this.buildFormData();

    this.saleOfferService.put(this.offerId()!, payload).subscribe({
      next: () => {
        this.toastService.success("Successfully edited offer!");
      },
      error: (err: ProblemDetails) => {
        this.toastService.success(err?.detail ?? "Failed to edit offer, try again later");
      }
    })
  }

  onDeliveryToggle(id: Guid, checked: boolean): void {
    const array = this.form.get('otherDeliveriesIds') as FormArray
    if (checked) array.push(new FormControl(id));
    else {
      const index = array.controls.findIndex(c => c.value === id);
      if (index !== -1) array.removeAt(index);
    }
    array.updateValueAndValidity();
  }

  getErrorMessage(path: string) {
    return getErrorMessage(this.form, path);
  }

  getImagePath(path: string | undefined) {
    if (!path) return '';
    return this.imageBaseUrl + path;
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
            alt: '',
            isNew: true
          }
        ]);
      };
      reader.readAsDataURL(file);
    }
    input.value = '';
  }


  removeImage(index: number): void {
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

  revertDelete(index: number): void{
    this.images.update(imgs => {
      const img = imgs[index];
      if(img.isDeleted)
        img.isDeleted = false;
      return [...imgs];
    });
  }
}
