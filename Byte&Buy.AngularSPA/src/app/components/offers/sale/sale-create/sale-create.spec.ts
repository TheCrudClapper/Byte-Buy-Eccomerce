import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleCreate } from './sale-create';

describe('SaleCreate', () => {
  let component: SaleCreate;
  let fixture: ComponentFixture<SaleCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleCreate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleCreate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
