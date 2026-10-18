import { DatePipe } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Wedding } from './features/wedding/models/wedding';
import { WeddingService } from './features/wedding/services/wedding';
import { WeddingSelectorComponent } from './features/wedding/components/wedding-selector/wedding-selector';
import { WeddingForm } from './features/wedding/components/wedding-form/wedding-form';
import { WeddingTaskManager } from './features/wedding-tasks/wedding-task-manager';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, DatePipe, WeddingSelectorComponent, WeddingForm, WeddingTaskManager],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private readonly weddingService = inject(WeddingService);
  private readonly changeDetector = inject(ChangeDetectorRef);

  weddings: Wedding[] = [];
  selectedWeddingId: string | null = null;
  selectedWedding: Wedding | null = null;
  editingWedding: Wedding | null = null;
  isWeddingFormOpen = false;
  isLoading = true;
  loadError = '';

  ngOnInit(): void {
    this.loadWeddings();
  }

  selectWedding(weddingId: string): void {
    this.selectedWeddingId = weddingId;
    this.selectedWedding = this.weddings.find((wedding) => wedding.id === weddingId) ?? null;
  }

  openCreateForm(): void {
    this.editingWedding = null;
    this.isWeddingFormOpen = true;
  }

  openEditForm(): void {
    if (!this.selectedWedding) return;
    this.editingWedding = this.selectedWedding;
    this.isWeddingFormOpen = true;
  }

  closeWeddingForm(): void {
    this.isWeddingFormOpen = false;
    this.editingWedding = null;
  }

  onWeddingSaved(weddingId: string): void {
    this.isWeddingFormOpen = false;
    this.editingWedding = null;
    this.loadWeddings(weddingId);
  }

  private loadWeddings(preferredWeddingId?: string): void {
    this.isLoading = true;
    this.loadError = '';
    this.weddingService.getAllWeddings().subscribe({
      next: (weddings) => {
        this.weddings = weddings;
        const selectedId = preferredWeddingId ?? this.selectedWeddingId ?? weddings[0]?.id ?? null;
        this.selectedWeddingId = selectedId;
        this.selectedWedding = weddings.find((wedding) => wedding.id === selectedId) ?? null;
        this.isLoading = false;
        this.changeDetector.markForCheck();
      },
      error: () => {
        this.loadError =
          'We could not load your weddings. Check that the API is running and try again.';
        this.isLoading = false;
        this.changeDetector.markForCheck();
      },
    });
  }
}
