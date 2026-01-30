import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentGateway } from './payment-gateway';

describe('PaymentGateway', () => {
  let component: PaymentGateway;
  let fixture: ComponentFixture<PaymentGateway>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PaymentGateway]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentGateway);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
