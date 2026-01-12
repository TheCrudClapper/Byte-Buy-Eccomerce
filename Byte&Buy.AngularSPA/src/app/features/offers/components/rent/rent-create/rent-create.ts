import { Component, inject, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { ReactiveFormsModule, FormGroup, Validators, FormControl, FormArray } from '@angular/forms';
import { SelectListItemResponse } from '../../../../../shared/api-dto/select-list-item-response';
import { getErrorMessage } from '../../../../../core/helpers/form-helper';
import { CategoryApiService } from '../../../../../core/services/category/category-api-service';
import { ConditionApiService } from '../../../../../core/services/condition/condition-api-service';
import { firstValueFrom, Observable, take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { SelectListItem } from '../../../../../shared/models/select-list-item';
import { DeliveryApiService } from '../../../../../core/services/delivery/delivery-api-service';
import { DeliveryListItem } from '../../../../../shared/models/delivery-list-items';
import { AddressApiService } from '../../../../../core/services/address/address-api-service';
import { HomeAddressDto } from '../../../../../shared/api-dto/home-address-dto';

@Component({
  selector: 'app-rent-create',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './rent-create.html',
  standalone: true,
  styleUrls: [
    './rent-create.scss',
    '../../../shared/offers-shared-styles.scss']
})
export class RentCreate implements OnInit{
  private categoryApiService = inject(CategoryApiService);
  private conditionApiService = inject(ConditionApiService);
  private deliveryApiService = inject(DeliveryApiService);
  private addressApiService = inject(AddressApiService);

  categoriesOptions$!: Observable<SelectListItem[]>;
  conditionOptions$!: Observable<SelectListItem[]>;
  deliveryOptions$!: Observable<DeliveryListItem[]>;
  homeAddress!: HomeAddressDto;

  rentForm : FormGroup = new FormGroup({
    name: new FormControl("", [Validators.required, Validators.maxLength(75), Validators.min(16)]),
    selectedCategoryId: new FormControl<string | null>(null, [Validators.required]),
    selectedConditionId: new FormControl<string | null>(null, [Validators.required]),
    pricePerDay: new FormControl<number>(0.00, [Validators.required, Validators.min(1)]),
    quantityAvailable: new FormControl<number>(0, [Validators.required, Validators.min(1)]),
    maxRentalDays: new FormControl<number>(0, [Validators.required, Validators.min(1)]),
    description: new FormControl<string>("", [Validators.maxLength(2000), Validators.required]),
    quantityAvaliable: new FormControl<number>(0, [Validators.min(1), Validators.required]), 
    selectedParcelLockerIds: new FormArray<FormControl<Guid>>([]),
    selectedOtherDeliveriesIds: new FormArray<FormControl<Guid>>([], [Validators.required, Validators.minLength(1)]),
    images: new FormControl<File[] | null>(null, [Validators.required, Validators.minLength(1)])
  });
  
  async ngOnInit(): Promise<void> {
    this.categoriesOptions$ = this.categoryApiService.getSelectList();
    this.conditionOptions$ = this.conditionApiService.getSelectList();
    this.deliveryOptions$ = this.deliveryApiService.getDeliveriesList(); 
  }

  onSubmit() :void{
    console.log(this.rentForm);
    if(this.rentForm.invalid){
      this.rentForm.markAllAsTouched();
      return;
    };
  }

  get selectedOtherDeliveriesIds() : FormArray{
    return this.rentForm.get('selectedOtherDeliveriesIds') as FormArray;
  }

  isSelected(id: Guid | string): boolean {
  return this.selectedOtherDeliveriesIds.value.includes(id);
  }

  toggleDelivery(event: Event, id: Guid | string){
    const target = event.target as HTMLInputElement;
    if(target.checked){
      this.selectedOtherDeliveriesIds.push(new FormControl(id));
    } else {
      const index = this.selectedOtherDeliveriesIds.controls.findIndex(ctrl => ctrl.value === id);
      if(index != -1) this.selectedOtherDeliveriesIds.removeAt(index);
    }
  }

  getErrorMessage(path: string){
    return getErrorMessage(this.rentForm, path);
  }}


