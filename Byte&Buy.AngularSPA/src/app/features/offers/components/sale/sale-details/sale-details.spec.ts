import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleDetails } from './sale-details';

describe('SaleDetails', () => {
  let component: SaleDetails;
  let fixture: ComponentFixture<SaleDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
