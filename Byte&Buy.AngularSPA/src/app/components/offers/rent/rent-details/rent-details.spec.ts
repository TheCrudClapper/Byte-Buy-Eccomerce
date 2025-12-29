import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentDetails } from './rent-details';

describe('RentDetails', () => {
  let component: RentDetails;
  let fixture: ComponentFixture<RentDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentDetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentDetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
