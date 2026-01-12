import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentEdit } from './rent-edit';

describe('RentEdit', () => {
  let component: RentEdit;
  let fixture: ComponentFixture<RentEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentEdit]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentEdit);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
