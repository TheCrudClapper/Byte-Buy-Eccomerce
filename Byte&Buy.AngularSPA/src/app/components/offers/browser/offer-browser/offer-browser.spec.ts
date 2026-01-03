import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferBrowser } from './offer-browser';

describe('OfferBrowser', () => {
  let component: OfferBrowser;
  let fixture: ComponentFixture<OfferBrowser>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfferBrowser]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfferBrowser);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
