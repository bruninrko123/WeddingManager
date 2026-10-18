import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { WeddingTask, CreateWeddingTask, UpdateWeddingTask } from '../models/wedding-task';

@Injectable({
  providedIn: 'root',
})
export class WeddingTaskService {
  private http = inject(HttpClient);

  private apiUrl = environment.apiUrl;

  getTasks(weddingId: string): Observable<WeddingTask[]> {
    return this.http.get<WeddingTask[]>(`${this.apiUrl}/api/weddings/${weddingId}/tasks`);
  }

  createTask(weddingId: string, task: CreateWeddingTask): Observable<WeddingTask> {
    return this.http.post<WeddingTask>(`${this.apiUrl}/api/weddings/${weddingId}/tasks`, task);
  }

  updateTask(weddingId: string, taskId: string, task: UpdateWeddingTask): Observable<WeddingTask> {
    return this.http.put<WeddingTask>(
      `${this.apiUrl}/api/weddings/${weddingId}/tasks/${taskId}`,
      task,
    );
  }

  deleteTask(weddingId: string, taskId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/weddings/${weddingId}/tasks/${taskId}`);
  }
}
