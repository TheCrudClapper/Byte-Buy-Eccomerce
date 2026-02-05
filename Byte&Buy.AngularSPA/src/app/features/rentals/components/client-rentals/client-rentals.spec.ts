import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientRentals } from './client-rentals';

describe('ClientRentals', () => {
  let component: ClientRentals;
  let fixture: ComponentFixture<ClientRentals>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientRentals]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientRentals);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
