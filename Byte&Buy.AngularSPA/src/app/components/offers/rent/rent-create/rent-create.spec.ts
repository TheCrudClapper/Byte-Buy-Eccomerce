import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentCreate } from './rent-create';

describe('RentCreate', () => {
  let component: RentCreate;
  let fixture: ComponentFixture<RentCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentCreate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentCreate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
