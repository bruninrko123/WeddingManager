import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { WeddingForm } from './wedding-form';

describe('WeddingForm', () => {
  let component: WeddingForm;
  let fixture: ComponentFixture<WeddingForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeddingForm],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();

    fixture = TestBed.createComponent(WeddingForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
