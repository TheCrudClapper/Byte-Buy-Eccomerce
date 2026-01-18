import { TestBed } from '@angular/core/testing';
import { CanActivateFn, CanMatchFn } from '@angular/router';

import { guidParameterGuard } from './guid-parameter-guard';

describe('guidParameterGuard', () => {
  const executeGuard: CanMatchFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => guidParameterGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
