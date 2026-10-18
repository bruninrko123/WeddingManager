import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeddingSelectorComponent } from './wedding-selector';

describe('WeddingSelectorComponent', () => {
  let component: WeddingSelectorComponent;
  let fixture: ComponentFixture<WeddingSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeddingSelectorComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(WeddingSelectorComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('emits the selected wedding id', () => {
    const weddingId = '11111111-1111-1111-1111-111111111111';
    fixture.componentRef.setInput('weddings', [
      { id: weddingId, name: 'Maya & Luca', weddingDate: '2027-06-12', location: 'Porto' },
    ]);
    fixture.detectChanges();
    const select = fixture.nativeElement.querySelector('select') as HTMLSelectElement;
    let emittedId = '';
    component.weddingSelected.subscribe((id) => (emittedId = id));

    select.value = weddingId;
    select.dispatchEvent(new Event('change'));

    expect(emittedId).toBe(weddingId);
  });
});
