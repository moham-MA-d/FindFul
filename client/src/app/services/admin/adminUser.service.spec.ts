/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AdminUserService } from './adminUser.service';

describe('Service: AdminUser', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminUserService]
    });
  });

  it('should ...', inject([AdminUserService], (service: AdminUserService) => {
    expect(service).toBeTruthy();
  }));
});
