import { Component, inject, OnInit, signal } from '@angular/core';
import { PaymentApiService } from '../../../../core/clients/payment/payment-api-service';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { PaymentModel } from '../../models/payment-model';
import { mapToPaymentModel } from '../../mappers/payment-mapper';
import { map } from 'rxjs';

@Component({
  selector: 'app-payment-gateway',
  imports: [],
  templateUrl: './payment-gateway.html',
  styleUrl: './payment-gateway.scss',
})
export class PaymentGateway implements OnInit {
  private readonly paymentApiService = inject(PaymentApiService);
  protected readonly route = inject(ActivatedRoute);
  private readonly paymentId = signal<Guid | undefined>(undefined);
  public paymentModel = signal<PaymentModel | undefined>(undefined);

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
      .subscribe(payment => {
        this.paymentModel.set(payment);
      });
  }

}
