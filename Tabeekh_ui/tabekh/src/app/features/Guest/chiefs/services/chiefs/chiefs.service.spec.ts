import { TestBed } from '@angular/core/testing';

import { chiefsService } from './chiefs.service';

describe('chiefsService', () => {
  let service: chiefsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(chiefsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
