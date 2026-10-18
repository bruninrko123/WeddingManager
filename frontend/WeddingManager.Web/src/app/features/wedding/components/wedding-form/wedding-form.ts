import { Component, EventEmitter, Input, OnChanges, Output, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { map } from 'rxjs';
import { Wedding } from '../../../wedding/models/wedding';
import { WeddingService } from '../../../wedding/services/wedding';

@Component({
  selector: 'app-wedding-form',
  imports: [ReactiveFormsModule],
  templateUrl: './wedding-form.html',
  styleUrl: './wedding-form.css',
})
export class WeddingForm implements OnChanges {
  private readonly weddingService = inject(WeddingService);

  weddingForm = new FormGroup({
    name: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    weddingDate: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    location: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
  });

  @Input() wedding: Wedding | null = null;
  @Output() saved = new EventEmitter<string>();
  @Output() cancelled = new EventEmitter<void>();

  isSaving = false;
  errorMessage = '';

  ngOnChanges(): void {
    this.errorMessage = '';
    this.weddingForm.reset({
      name: this.wedding?.name ?? '',
      weddingDate: this.wedding?.weddingDate ?? '',
      location: this.wedding?.location ?? '',
    });
  }

  onSubmit(): void {
    if (this.weddingForm.invalid || !this.weddingForm.controls.weddingDate.value) {
      this.weddingForm.markAllAsTouched();
      return;
    }

    const weddingData = this.weddingForm.getRawValue();
    const request = this.wedding
      ? this.weddingService.updateWedding(this.wedding.id, weddingData).pipe(map(() => this.wedding!.id))
      : this.weddingService.createWedding(weddingData).pipe(map((result) => result.id));

    this.isSaving = true;
    this.errorMessage = '';
    request.subscribe({
      next: (weddingId) => {
        this.isSaving = false;
        this.saved.emit(weddingId);
      },
      error: () => {
        this.isSaving = false;
        this.errorMessage = 'We could not save these details. Please try again.';
      },
    });
  }
}
