import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Fobidden } from './fobidden';

describe('Fobidden', () => {
  let component: Fobidden;
  let fixture: ComponentFixture<Fobidden>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Fobidden]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Fobidden);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
