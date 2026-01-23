import { Component, DestroyRef, EventEmitter, inject, Input, input, OnInit, Output } from '@angular/core';
import { CartApiService } from '../../../../../core/clients/cart/cart-api-service';
import { SaleCartOfferModel } from '../../../models/cart-offers/sale-cart-offer-model';
import { DecimalPipe } from '@angular/common';
import { FormGroup, ɵInternalFormsSharedModule, ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { SaleCartOfferUpdateRequest } from '../../../../../core/dto/cart/cart-item/sale-cart-offer-update-request';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { CartSummary } from '../../../models/cart-summary';
import { Guid } from 'guid-typescript';
import { RouterLink } from '@angular/router';
import { environment } from '../../../../../../environments/environment';

@Component({
  selector: 'app-sale-cart-offer',
  imports: [DecimalPipe, ɵInternalFormsSharedModule, ReactiveFormsModule, RouterLink],
  templateUrl: './sale-cart-offer.html',
  styleUrls: ['./sale-cart-offer.scss',
    '../../../shared/styles/card-shared-styles.scss'
  ],
  standalone: true,
})
export class SaleCartOffer implements OnInit {
  @Input() saleCartOffer!: SaleCartOfferModel;
  @Output() summaryUpdated = new EventEmitter<CartSummary>();
  @Output() removed = new EventEmitter<Guid>();

  private readonly cartApiService = inject(CartApiService);
  private readonly toastService = inject(ToastService);
  protected readonly imageBaseUrl = environment.staticImagesBaseUrl;

  cartForm!: FormGroup;

  constructor(private builder: FormBuilder, private destroyRef: DestroyRef) { }

  ngOnInit(): void {
    this.cartForm = this.builder.group({
      quantity: [this.saleCartOffer.quantity, [Validators.required]]
    })

    this.cartForm.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(
        (a, b) => a.quantity == b.quantity
      ),
      takeUntilDestroyed(this.destroyRef))
      .subscribe(x => {
        if (x.quantity <= 0) {
          this.tryRemove(this.saleCartOffer.id);
          return;
        }

        if (this.cartForm.valid)
          this.tryUpdate(x.quantity);
      });
  }

  increment(): void {
    const ctrl = this.cartForm.get('quantity')!;
    ctrl.setValue(ctrl.value + 1);
  }

  decrement() {
    const ctrl = this.cartForm.get('quantity')!;
    ctrl.setValue(ctrl.value - 1);
  }

  tryUpdate(quantity: number): void {
    const payload: SaleCartOfferUpdateRequest = {
      quantity: quantity
    }

    this.cartApiService.putSaleCartOffer(this.saleCartOffer.id, payload)
      .subscribe({
        next: (data: CartSummary) => {
          this.summaryUpdated.emit(data);
          //Locally updating subtotal 
          this.saleCartOffer.subtotal = {
            ...this.saleCartOffer.subtotal,
            amount: quantity * this.saleCartOffer.pricePerItem.amount
          }
          this.toastService.success("Updated Cart !");
        },
        error: (err: ProblemDetails) => {
          this.toastService.error(err?.detail ?? "Failed to update quantity");
        }
      })
  }

  tryRemove(id: Guid) {
    this.cartApiService.deleteCartOffer(id).subscribe({
      next: (data: CartSummary) => {
        this.summaryUpdated.emit(data);
        this.removed.emit(id);
        this.toastService.success("Item removed from cart");
      },
      error: (err: ProblemDetails) => {
        this.toastService.error(err?.detail ?? "Failed to delete cart item");
      }
    });
  }


}
