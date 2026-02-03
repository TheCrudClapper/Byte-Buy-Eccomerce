import { Component, inject, OnInit, signal } from '@angular/core';
import { PaymentApiService } from '../../../../core/clients/payment/payment-api-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { PaymentModel } from '../../models/payment-model';
import { mapToPaymentModel } from '../../mappers/payment-mapper';
import { map } from 'rxjs';
import { DecimalPipe } from '@angular/common';
import { PaymentMethod } from '../../models/payment-method';
import { FormBuilder, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { BlikPaymentRequest } from '../../../../core/dto/payment/blik-payment-request';
import { CardPaymentRequest } from '../../../../core/dto/payment/card-payment-request';
import { ToastService } from '../../../../shared/services/snackbar/toast-service';
import { ProblemDetails } from '../../../../core/dto/problem-details';
import { getErrorMessage } from '../../../../shared/helpers/form-helper';

@Component({
  selector: 'app-payment-gateway',
  imports: [DecimalPipe, ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './payment-gateway.html',
  standalone: true,
  styleUrl: './payment-gateway.scss',
})
export class PaymentGateway implements OnInit {
  private readonly paymentApiService = inject(PaymentApiService);
  protected readonly route = inject(ActivatedRoute);
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly toastService = inject(ToastService);

  private readonly paymentId = signal<Guid | undefined>(undefined);
  public paymentModel = signal<PaymentModel | undefined>(undefined);

  //for template only
  readonly PaymentMethod = PaymentMethod;
  readonly form = this.fb.group({

    cardNumber: [''],
    expiration: [''],
    cvc: [''],
    cardHolder: [''],


    phone: [''],
    blikCode: [''],
  });


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) return;

    const guid = Guid.parse(id);
    this.paymentId.set(guid);
    this.loadPayment(guid);
  }

  private loadPayment(id: Guid): void {
    this.paymentApiService
      .getPaymentResponse(id)
      .pipe(
        map(mapToPaymentModel)
      )
      .subscribe({
        next: (payment) => {
          this.paymentModel.set(payment)
          this.buildForm(payment.method)
        },
        error: () => {
          this.router.navigate(['/not-found']);
        }
      })
  }
  private buildForm(method: PaymentMethod) {
    this.form.reset();

    Object.values(this.form.controls).forEach(control => {
      control.clearValidators();
      control.updateValueAndValidity({ emitEvent: false });
    });

    if (method === PaymentMethod.Card) {
      this.form.controls.cardNumber.setValidators([
        Validators.required,
        Validators.minLength(16)
      ]);
      this.form.controls.expiration.setValidators(Validators.required);
      this.form.controls.cvc.setValidators([
        Validators.required,
        Validators.minLength(3),
      ]);
      this.form.controls.cardHolder.setValidators(Validators.required);
    }

    if (method === PaymentMethod.Blik) {
      this.form.controls.phone.setValidators([
        Validators.required,
      ]);
      this.form.controls.blikCode.setValidators([
        Validators.required,
        Validators.pattern(/^\d{6}$/),
      ]);
    }
    this.form.updateValueAndValidity();
  }

  getErrorMessage(path: string) {
    return getErrorMessage(this.form, path);
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const model = this.paymentModel();
    if (!model) return;

    const id = this.paymentId();
    if (!id) return;

    if (model.method === PaymentMethod.Card) {
      const payload: CardPaymentRequest = {
        cardHolderName: this.form.value.cardHolder!,
        cardNumber: this.form.value.cardNumber!
      }

      this.paymentApiService.payWithCard(id, payload).subscribe({
        next: () => {
          this.toastService.success("Successfully paid for order with Card");
          this.router.navigate(['/profile', 'my-orders']);
        },
        error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to pay using card")
      });
    }

    if (model.method === PaymentMethod.Blik) {
      const payload: BlikPaymentRequest = {
        phoneNumber: this.form.value.phone!
      }

      this.paymentApiService.payWithBlik(id, payload).subscribe({
        next: () => {
          this.toastService.success("Successfully paid for order with Blik !")
          this.router.navigate(['/profile', 'my-orders']);
        },
        error: (err: ProblemDetails) => this.toastService.error(err?.detail ?? "Failed to pay using blik")
      });
    }
  }
}
