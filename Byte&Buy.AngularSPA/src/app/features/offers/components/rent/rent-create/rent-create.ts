import { Component, inject, OnInit, signal } from '@angular/core';
import { Guid } from 'guid-typescript';
import { ReactiveFormsModule, FormGroup, Validators, FormControl, FormArray, FormsModule } from '@angular/forms';
import { getErrorMessage } from '../../../../../core/helpers/form-helper';
import { CategoryApiService } from '../../../../../core/services/category/category-api-service';
import { ConditionApiService } from '../../../../../core/services/condition/condition-api-service';
import { CommonModule } from '@angular/common';
import { SelectListItem } from '../../../../../shared/models/select-list-item';
import { DeliveryApiService } from '../../../../../core/services/delivery/delivery-api-service';
import { DeliveryListItem } from '../../../../../shared/models/delivery-list-items';
import { AddressApiService } from '../../../../../core/services/address/address-api-service';
import { HomeAddressDto } from '../../../../../shared/api-dto/home-address-dto';
import { RentOfferApiSerivce } from '../../../services/rent-offer-api-serivce';
import { map, Observable, single } from 'rxjs';
import { ImageItem } from '../../../models/image-item';
import { mapToListItem } from '../../../../../shared/mappers/offer-mappers';
import { SnackbarService } from '../../../../../core/services/snackbar/snackbar-service';

@Component({
  selector: 'app-rent-create',
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './rent-create.html',
  standalone: true,
  styleUrls: [
    './rent-create.scss',
    '../../../shared/offers-shared-styles.scss']
})
export class RentCreate implements OnInit {
  private readonly categoryApiService = inject(CategoryApiService);
  private readonly conditionApiService = inject(ConditionApiService);
  private readonly deliveryApiService = inject(DeliveryApiService);
  private readonly addressApiService = inject(AddressApiService);
  private readonly rentOfferApiService = inject(RentOfferApiSerivce);
  private readonly snackbarService: SnackbarService = inject(SnackbarService);

  categoriesOptions$!: Observable<SelectListItem[]>;
  conditionOptions$!: Observable<SelectListItem[]>;
  parcelLockerDeliveries = signal<DeliveryListItem[]>([])
  pickupPointDeliveries = signal<DeliveryListItem[]>([]);
  courierDeliveries = signal<DeliveryListItem[]>([]);

  homeAddress = signal<HomeAddressDto | null>(null); 
  images: ImageItem[] = [];

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
        this.parcelLockerDeliveries.set(response.parcelLockerDeliveries.map(mapToListItem));
        this.courierDeliveries.set(response.courierDeliveries.map(mapToListItem));
        this.pickupPointDeliveries.set(response.pickupPointDeliveries.map(mapToListItem));
      });
  }

  private buildFormData(): FormData {
    const form = this.rentForm.value;
    const fd = new FormData();

    fd.append('CategoryId', form.selectedCategoryId || '');
    fd.append('ConditionId', form.selectedConditionId || '');
    fd.append('Name', form.name);
    fd.append('Description', form.description);
    fd.append('QuantityAvailable', form.quantityAvailable.toString());
    fd.append('PricePerDay', form.pricePerDay.toString());
    fd.append('MaxRentalDays', form.maxRentalDays.toString());

    this.images.forEach((img, index) => {
      fd.append(`Images[${index}].Image`, img.file, img.file.name);
      fd.append(`Images[${index}].AltText`, img.alt || '');
    });

    form.otherDeliveriesIds.forEach((id: Guid, index: number) => {
      fd.append(`OtherDeliveriesIds[${index}]`, id.toString());
    });
    return fd;
  }

  onSubmit(): void {
    if (this.rentForm.invalid) {
      this.rentForm.markAllAsTouched();
      return;
    };

    if (!this.homeAddress()) {
      this.snackbarService.success("To add offer you need to add address !");
      return;
    }

    const payload = this.buildFormData();

    this.rentOfferApiService.post(payload).subscribe({
      next: () => {
        this.snackbarService.success("Successfully published offer!");
      },
      error: () => {
        this.snackbarService.success("Gowno nie dziala");
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
        this.images.push({
          file,
          preview: reader.result as string,
          alt: ''
        });
      };
      reader.readAsDataURL(file);
    }
    input.value = '';
  }

  removeImage(index: number): void {
    this.images.splice(index, 1);
  }
}





