import { TestBed } from '@angular/core/testing';

import { CartManageService } from './cart-manage.service';

describe('CartManageService', () => {
  let service: CartManageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CartManageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
