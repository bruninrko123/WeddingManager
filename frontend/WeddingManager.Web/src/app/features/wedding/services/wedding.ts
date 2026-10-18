import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { CreateWedding, Wedding } from '../models/wedding';
@Injectable({
  providedIn: 'root',
})
export class WeddingService {
  private http = inject(HttpClient);

  private apiUrl = environment.apiUrl;

  getWedding(weddingId: string): Observable<Wedding> {
    return this.http.get<Wedding>(`${this.apiUrl}/api/Wedding/GetWeddingById/${weddingId}`);
  }

  getAllWeddings(): Observable<Wedding[]> {
    return this.http.get<Wedding[]>(`${this.apiUrl}/api/Wedding/GetAllWeddings`);
  }

  createWedding(wedding: CreateWedding): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/api/Wedding/CreateWedding`, wedding);
  }

  updateWedding(weddingId: string, wedding: CreateWedding): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/api/Wedding/UpdateWedding/${weddingId}`, wedding);
  }

  deleteWedding(weddingId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/Wedding/DeleteWedding/${weddingId}`);
  }
}
