import { TestBed } from '@angular/core/testing';

import { CompanyInfoApiService } from './company-info-api-service';

describe('CompanyInfoApiService', () => {
  let service: CompanyInfoApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompanyInfoApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
