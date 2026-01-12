import { Component, inject } from '@angular/core';
import { Guid } from 'guid-typescript';
import { ReactiveFormsModule, FormGroup, Validators, FormControl, FormArray } from '@angular/forms';
import { SelectListItemResponse } from '../../../../../shared/api-dto/select-list-item-response';
import { ShowErrorPipe } from '../../../../../core/pipes/show-error-pipe';

@Component({
  selector: 'app-rent-create',
  imports: [ReactiveFormsModule, ShowErrorPipe],
  templateUrl: './rent-create.html',
  standalone: true,
  styleUrls: [
    './rent-create.scss',
    '../../../shared/offers-shared-styles.scss']
})
export class RentCreate {
  categories: SelectListItemResponse[] = [
    { id: Guid.create(), title: "Inpost Parcel Locker - L"}
  ]

  conditions: SelectListItemResponse[] = [
    {id: Guid.create(), title: "New"}
  ]
  
  rentForm : FormGroup = new FormGroup({
    name: new FormControl("", [Validators.required, Validators.maxLength(75), Validators.min(16)]),
    selectedCategoryId: new FormControl<Guid | null>(null, [Validators.required]),
    selectedConditionId: new FormControl<Guid | null>(null, [Validators.required]),
    selectedAddressId: new FormControl<Guid | null>(null, [Validators.required]),
    pricePerDay: new FormControl<number>(0.00, [Validators.required, Validators.min(1)]),
    quantityAvailable: new FormControl<number>(0, [Validators.required, Validators.min(1)]),
    maxRentalDays: new FormControl<number>(0, [Validators.required, Validators.min(1)]),
    description: new FormControl<string>("", [Validators.maxLength(2000)]),
    stockQuantity: new FormControl<number>(0, [Validators.min(1), Validators.required]), 
    selectedParcelLockerIds: new FormArray<FormControl<Guid>>([]),
    selectedOtherDeliveriesIds: new FormArray<FormControl<Guid>>([], [Validators.required, Validators.minLength(1)]),
    images: new FormControl<File[] | null>(null, [Validators.required, Validators.minLength(1)])
  });
  
  submit() :void{
    console.log(56);
    if(this.rentForm.invalid){
      this.rentForm.markAllAsTouched();
      return;
    };
  }

}


