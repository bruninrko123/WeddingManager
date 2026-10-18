import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { WeddingService } from './wedding';

describe('WeddingService', () => {
  let service: WeddingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(WeddingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
