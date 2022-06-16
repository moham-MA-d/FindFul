/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { OnlineService } from './Online.service';

describe('Service: Online', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OnlineService]
    });
  });

  it('should ...', inject([OnlineService], (service: OnlineService) => {
    expect(service).toBeTruthy();
  }));
});
