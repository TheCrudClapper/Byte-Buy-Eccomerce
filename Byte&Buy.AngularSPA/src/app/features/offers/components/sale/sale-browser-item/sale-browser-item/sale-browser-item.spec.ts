import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleBrowserItem } from './sale-browser-item';

describe('SaleBrowserItem', () => {
  let component: SaleBrowserItem;
  let fixture: ComponentFixture<SaleBrowserItem>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SaleBrowserItem]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleBrowserItem);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
