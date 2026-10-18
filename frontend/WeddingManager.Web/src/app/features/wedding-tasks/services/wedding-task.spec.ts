import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { WeddingTaskService } from './wedding-task';

describe('WeddingTaskService', () => {
  let service: WeddingTaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(WeddingTaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
