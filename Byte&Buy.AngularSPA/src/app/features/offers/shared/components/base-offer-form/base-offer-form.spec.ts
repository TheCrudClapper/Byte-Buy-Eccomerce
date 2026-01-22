import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseOfferForm } from './base-offer-form';

describe('BaseOfferForm', () => {
  let component: BaseOfferForm;
  let fixture: ComponentFixture<BaseOfferForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BaseOfferForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BaseOfferForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
