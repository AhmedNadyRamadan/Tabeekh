import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { chiefGuard } from './chief.guard';

describe('chiefGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => chiefGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
