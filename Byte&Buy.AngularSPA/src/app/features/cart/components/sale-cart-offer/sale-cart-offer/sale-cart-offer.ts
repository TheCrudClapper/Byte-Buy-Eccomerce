import { Component, DestroyRef, EventEmitter, inject, Input, input, OnInit, Output } from '@angular/core';
import { CartApiService } from '../../../../../core/clients/cart/cart-api-service';
import { SaleCartOfferModel } from '../../../models/cart-offers/sale-cart-offer-model';
import { DecimalPipe } from '@angular/common';
import { FormGroup, ɵInternalFormsSharedModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { ToastService } from '../../../../../shared/services/snackbar/toast-service';
import { debounceTime} from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { SaleCartOfferUpdateRequest } from '../../../../../core/dto/cart/cart-item/sale-cart-offer-update-request';
import { ProblemDetails } from '../../../../../core/dto/problem-details';
import { CartSummary } from '../../../models/cart-summary';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-sale-cart-offer',
  imports: [DecimalPipe, ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './sale-cart-offer.html',
  styleUrl: './sale-cart-offer.scss',
  standalone: true,
})
export class SaleCartOffer implements OnInit {
  @Input() saleCartOffer!: SaleCartOfferModel;
  @Output() summaryUpdated = new EventEmitter<CartSummary>();
  @Output() removed = new EventEmitter<Guid>();

  private lastValidQuantity!: number;

  private readonly cartApiService = inject(CartApiService);
  private readonly toastService = inject(ToastService);
  protected readonly imageBaseUrl = "http://localhost:5099/Images/";

  cartForm!: FormGroup;

  constructor(private builder: FormBuilder, private destroyRef: DestroyRef) { }

  ngOnInit(): void {
    this.cartForm = this.builder.group({
      quantity: [this.saleCartOffer.quantity]
    })

    this.lastValidQuantity = this.saleCartOffer.quantity;

    this.cartForm.valueChanges.pipe(
      debounceTime(400),
      takeUntilDestroyed(this.destroyRef))
      .subscribe(x => {
        if (this.cartForm.valid) { this.onQuantityChanged(x.quantity)}
      });
  }

  onQuantityChanged(quantity: number): void{
    if(quantity <= 0)
      this.tryRemove();
    else
      this.tryUpdate(quantity);
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
          this.rollbackQuantity();
          this.toastService.error(err?.detail ?? "Failed to update quantity");
        }
      })
  }

  tryRemove() {
    this.cartApiService.deleteCartOffer(this.saleCartOffer.id).subscribe({
      next: (data: CartSummary) => {
        this.summaryUpdated.emit(data);
        this.removed.emit(this.saleCartOffer.id)
      },
      error: (err: ProblemDetails) => {
        this.toastService.error(err?.detail ?? "Failed to delete cart item");
      }
    });
  }

  private rollbackQuantity(): void{
    this.cartForm.patchValue({ quantity: this.lastValidQuantity }, { emitEvent: false });
  }
  
}
