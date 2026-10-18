import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Wedding } from '../../../wedding/models/wedding';

@Component({
  selector: 'app-wedding-selector',
  standalone: true,
  imports: [],
  templateUrl: './wedding-selector.html',
  styleUrl: './wedding-selector.css',
})
export class WeddingSelectorComponent {
  @Input() weddings: Wedding[] = [];
  @Input() selectedWeddingId: string | null = null;

  @Output() weddingSelected = new EventEmitter<string>();

  onWeddingChange(event: Event): void {
    const weddingId = (event.target as HTMLSelectElement).value;
    if (weddingId) {
      this.weddingSelected.emit(weddingId);
    }
  }
}
