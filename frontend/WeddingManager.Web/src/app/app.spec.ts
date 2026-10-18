import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { App } from './app';

describe('App', () => {
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    httpTestingController
      .expectOne((request) => request.url.endsWith('/api/Wedding/GetAllWeddings'))
      .flush([]);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render the planning dashboard', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    httpTestingController
      .expectOne((request) => request.url.endsWith('/api/Wedding/GetAllWeddings'))
      .flush([]);
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Make room for the good things.');
  });
});
