import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientOrders } from './client-orders';

describe('ClientOrders', () => {
  let component: ClientOrders;
  let fixture: ComponentFixture<ClientOrders>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientOrders]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientOrders);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
