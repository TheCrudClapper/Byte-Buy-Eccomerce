import { Component, inject, OnInit, signal } from '@angular/core';
import { Guid } from 'guid-typescript';
import { ReactiveFormsModule, FormGroup, Validators, FormControl, FormArray, FormsModule } from '@angular/forms';
import { getErrorMessage } from '../../../../../shared/helpers/form-helper';
import { CategoryApiService } from '../../../../../core/clients/category/category-api-service';
import { ConditionApiService } from '../../../../../core/clients/condition/condition-api-service';
import { CommonModule } from '@angular/common';
import { SelectListItem } from '../../../../../shared/models/select-list-item';
import { DeliveryApiService } from '../../../../../core/clients/delivery/delivery-api-service';
import { DeliveryListItem } from '../../../../../shared/models/delivery-list-items';
import { AddressApiService } from '../../../../../core/clients/address/address-api-service';
import { HomeAddressDto } from '../../../../../core/dto/home-address/home-address-dto';
import { RentOfferApiSerivce } from '../../../../../core/clients/offers/rent/rent-offer-api-serivce';
import { ImageItem } from '../../../models/image-item';
import { mapToListItem } from '../../../../../shared/mappers/offer-mappers';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-rent-create',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './rent-create.html',
  standalone: true,
  styleUrls: [
    './rent-create.scss',
    '../../../shared/styles/offers-shared-styles.scss']
})
export class RentCreate implements OnInit {
  private readonly categoryApiService = inject(CategoryApiService);
  private readonly conditionApiService = inject(ConditionApiService);
  private readonly deliveryApiService = inject(DeliveryApiService);
  private readonly addressApiService = inject(AddressApiService);
  private readonly rentOfferApiService = inject(RentOfferApiSerivce);
  private readonly toastService = inject(ToastService);

  categoriesOptions$!: Observable<SelectListItem[]>;
  conditionOptions$!: Observable<SelectListItem[]>;
  deliveries = signal<{ parcel: DeliveryListItem[], courier: DeliveryListItem[], pickup: DeliveryListItem[] }>({
    parcel: [],
    courier: [],
    pickup: []
  });

  homeAddress = signal<HomeAddressDto | null>(null);
  images = signal<ImageItem[]>([]);

  rentForm: FormGroup = new FormGroup({
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


  ngOnInit(): void {
    this.categoriesOptions$ = this.categoryApiService.getSelectList();
    this.conditionOptions$ = this.conditionApiService.getSelectList();

    this.addressApiService.getHomeAddress().subscribe(address => {
      this.homeAddress.set(address);
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

  private buildFormData(): FormData {
    const form = this.rentForm.value;
    const fd = new FormData();

    fd.append('CategoryId', form.selectedCategoryId!);
    fd.append('ConditionId', form.selectedConditionId!);
    fd.append('Name', form.name);
    fd.append('Description', form.description);
    fd.append('QuantityAvailable', String(form.quantityAvailable));
    fd.append('PricePerDay', String(form.pricePerDay));
    fd.append('MaxRentalDays', String(form.maxRentalDays));

    this.images().forEach((img, index) => {
      fd.append(`Images[${index}].Image`, img?.file ?? '');
      if(img.alt){
        fd.append(`Images[${index}].AltText`, img.alt);
      }
    });

    form.otherDeliveriesIds.forEach((id: Guid, index: number) => {
      fd.append(`OtherDeliveriesIds[${index}]`, String(id));
    });

    return fd;
  }

  onSubmit(): void {
    if(this.images().length === 0){
      this.toastService.error("Add at least one image");
      return;
    }

    if (this.rentForm.invalid) {
      this.rentForm.markAllAsTouched();
      return;
    };

    if (!this.homeAddress()) {
      this.toastService.success("To add offer you need to add address !");
      return;
    }

    const payload = this.buildFormData();

    this.rentOfferApiService.post(payload).subscribe({
      next: () => {
        this.toastService.success("Successfully published offer!");
      },
      error: (err: ProblemDetails) => {
        this.toastService.success(err?.detail ?? "Failed to publish offer, try again later");
      }
    })
  }

  onDeliveryToggle(id: Guid, checked: boolean): void {
    const array = this.rentForm.get('otherDeliveriesIds') as FormArray

    if (checked)
      array.push(new FormControl(id));
    else {
      const index = array.controls.findIndex(c => c.value === id);
      if (index !== -1) {
        array.removeAt(index);
      }
    }

    array.markAsTouched();
    array.updateValueAndValidity();
  }

  getErrorMessage(path: string) {
    return getErrorMessage(this.rentForm, path);
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
}





