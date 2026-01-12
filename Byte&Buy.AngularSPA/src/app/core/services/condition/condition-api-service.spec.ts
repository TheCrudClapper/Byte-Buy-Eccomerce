import { TestBed } from '@angular/core/testing';

import { ConditionApiService } from './condition-api-service';

describe('ConditionApiService', () => {
  let service: ConditionApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ConditionApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
