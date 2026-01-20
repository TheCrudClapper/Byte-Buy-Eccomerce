import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentBrowserItem } from './rent-browser-item';

describe('RentBrowserItem', () => {
  let component: RentBrowserItem;
  let fixture: ComponentFixture<RentBrowserItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RentBrowserItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentBrowserItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
