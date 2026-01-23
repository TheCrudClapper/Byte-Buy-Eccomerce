import { Component, DestroyRef, EventEmitter, inject, Input, input, OnInit, Output } from '@angular/core';
import { CartApiService } from '../../../../../core/clients/cart/cart-api-service';
import { RentCartOfferModel } from '../../../models/cart-offers/rent-cart-offer-model';
import { CartSummary } from '../../../models/cart-summary';
import { Guid } from 'guid-typescript';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { DecimalPipe } from '@angular/common';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { RentCartOfferUpdateRequest } from '../../../../../core/dto/cart/cart-item/rent-cart-offer-update-request';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-rent-cart-offer',
  imports: [DecimalPipe, ReactiveFormsModule, RouterLink],
  templateUrl: './rent-cart-offer.html',
  standalone: true,
  styleUrls: ['./rent-cart-offer.scss',
    '../../../shared/styles/card-shared-styles.scss'
  ]
})
export class RentCartOffer implements OnInit {
  @Input() rentCartOffer!: RentCartOfferModel;
  @Output() summaryUpdated = new EventEmitter<CartSummary>();
  @Output() removed = new EventEmitter<Guid>();

  private readonly cartApiService = inject(CartApiService);
  protected readonly imageBaseUrl = "http://localhost:5099/Images/";
  private readonly toastService = inject(ToastService);

  cartForm!: FormGroup;

  constructor(private builder: FormBuilder, private destroyRef: DestroyRef) { }

  ngOnInit(): void {
    this.cartForm = this.builder.group({
      quantity: [this.rentCartOffer.quantity, [Validators.required]],
      rentalDays: [this.rentCartOffer.rentalDays, [Validators.required, Validators.min(1)]]
    })

    this.cartForm.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(
        (a, b) => a.quantity === b.quantity && a.rentalDays === b.rentalDays
      ),
      takeUntilDestroyed(this.destroyRef))
      .subscribe(x => {
        if (x.quantity <= 0) {
          this.tryRemove(this.rentCartOffer.id);
          return;
        }

        if (this.cartForm.valid)
          this.tryUpdate(x.quantity, x.rentalDays)
      })
  }

  increment(): void {
    const ctrl = this.cartForm.get('quantity')!;
    ctrl.setValue(ctrl.value + 1);
  }

  decrement() {
    const ctrl = this.cartForm.get('quantity')!;
    ctrl.setValue(ctrl.value - 1);
  }

  incrementDays(): void {
    const ctrl = this.cartForm.get('rentalDays')!;
    ctrl.setValue(ctrl.value + 1);
  }

  decrementDays(): void {
    const ctrl = this.cartForm.get('rentalDays')!;
    if (ctrl.value > 1) {
      ctrl.setValue(ctrl.value - 1);
    }
  }

  tryRemove(id: Guid) {
    this.cartApiService.deleteCartOffer(id).subscribe({
      next: (data: CartSummary) => {
        this.summaryUpdated.emit(data);
        this.removed.emit(id)
      },
      error: (err: ProblemDetails) => {
        this.toastService.error(err?.detail ?? "Failed to delete cart item");
      }
    });
  }

  tryUpdate(quantity: number, rentalDays: number) {
    const data = this.cartForm.value;
    const payload: RentCartOfferUpdateRequest = {
      quantity: data.quantity,
      rentalDays: data.rentalDays
    }

    this.cartApiService.putRentCartOffer(this.rentCartOffer.id, payload).subscribe({
      next: (data: CartSummary) => {
        this.summaryUpdated.emit(data);
        //updating locally subtotal
        this.rentCartOffer.subtotal = {
          ...this.rentCartOffer.subtotal,
          amount: quantity * this.rentCartOffer.pricePerDay.amount * rentalDays
        }
        this.toastService.success("Updated Cart !");
      },
      error: (err: ProblemDetails) => {
        this.toastService.error(err?.detail ?? "Failed to update quantity");
      }
    });
  }
}
