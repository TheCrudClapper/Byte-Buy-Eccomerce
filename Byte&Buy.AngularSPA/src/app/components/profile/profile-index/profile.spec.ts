import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileIndex } from './profile-index';

describe('Profile', () => {
  let component: ProfileIndex;
  let fixture: ComponentFixture<ProfileIndex>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProfileIndex]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileIndex);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
