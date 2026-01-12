import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleEdit } from './sale-edit';

describe('SaleEdit', () => {
  let component: SaleEdit;
  let fixture: ComponentFixture<SaleEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleEdit]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleEdit);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
