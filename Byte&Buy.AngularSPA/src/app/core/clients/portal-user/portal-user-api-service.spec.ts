import { TestBed } from '@angular/core/testing';

import { PortalUserApiService } from './portal-user-api-service';

describe('PortalUserApiService', () => {
  let service: PortalUserApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PortalUserApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
