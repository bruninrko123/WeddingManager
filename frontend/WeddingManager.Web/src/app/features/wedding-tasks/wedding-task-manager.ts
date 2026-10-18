import { DatePipe } from '@angular/common';
import {
  ChangeDetectorRef,
  Component,
  Input,
  OnChanges,
  SimpleChanges,
  inject,
} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { WeddingTask, CreateWeddingTask, UpdateWeddingTask } from './models/wedding-task';
import { WeddingTaskService } from './services/wedding-task';

@Component({
  selector: 'app-wedding-task-manager',
  imports: [DatePipe, ReactiveFormsModule],
  templateUrl: './wedding-task-manager.html',
  styleUrl: './wedding-task-manager.css',
})
export class WeddingTaskManager implements OnChanges {
  private readonly taskService = inject(WeddingTaskService);
  private readonly changeDetector = inject(ChangeDetectorRef);

  @Input() weddingId: string | null = null;

  tasks: WeddingTask[] = [];
  editingTaskId: string | null = null;
  confirmingDeleteId: string | null = null;
  isLoading = false;
  isSaving = false;
  errorMessage = '';

  taskForm = new FormGroup({
    title: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    description: new FormControl('', { nonNullable: true }),
    dueDate: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
  });

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['weddingId']) {
      this.cancelEdit();
      this.loadTasks();
    }
  }

  loadTasks(): void {
    if (!this.weddingId) {
      this.tasks = [];
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.taskService.getTasks(this.weddingId).subscribe({
      next: (tasks) => {
        this.tasks = tasks;
        this.isLoading = false;
        this.changeDetector.markForCheck();
      },
      error: () => {
        this.isLoading = false;
        this.errorMessage = 'Tasks could not be loaded. Please try again.';
        this.changeDetector.markForCheck();
      },
    });
  }

  saveTask(): void {
    if (!this.weddingId) return;
    if (this.taskForm.invalid) {
      this.taskForm.markAllAsTouched();
        return;
    }

    const values = this.taskForm.getRawValue();
    const taskData = {
      title: values.title.trim(),
      description: values.description.trim() || null,
      dueDate: values.dueDate,
    };

    if (!taskData.title) {
      this.taskForm.controls.title.setErrors({ required: true });
      this.taskForm.controls.title.markAsTouched();
      return;
    }

    this.isSaving = true;
    this.errorMessage = '';
    const request = this.editingTaskId
      ? this.taskService.updateTask(this.weddingId, this.editingTaskId, {
          ...taskData,
          isCompleted:
            this.tasks.find((task) => task.id === this.editingTaskId)?.isCompleted ?? false,
        } satisfies UpdateWeddingTask)
      : this.taskService.createTask(this.weddingId, taskData satisfies CreateWeddingTask);

    request.subscribe({
      next: () => {
        this.isSaving = false;
        this.cancelEdit();
        this.loadTasks();
        this.changeDetector.markForCheck();
      },
      error: () => {
        this.isSaving = false;
        this.errorMessage = 'The task could not be saved. Please try again.';
        this.changeDetector.markForCheck();
      },
    });
  }

  editTask(task: WeddingTask): void {
    this.editingTaskId = task.id;
    this.confirmingDeleteId = null;
    this.taskForm.reset({
      title: task.title,
      description: task.description ?? '',
      dueDate: task.dueDate ?? '',
    });
  }

  cancelEdit(): void {
    this.editingTaskId = null;
    this.confirmingDeleteId = null;
    this.taskForm.reset({ title: '', description: '', dueDate: '' });
  }

  toggleCompleted(task: WeddingTask): void {
    if (!this.weddingId) return;
    this.taskService
      .updateTask(this.weddingId, task.id, {
        title: task.title,
        description: task.description,
        dueDate: task.dueDate ?? '',
        isCompleted: !task.isCompleted,
      })
      .subscribe({
        next: (updatedTask) => {
          this.tasks = this.tasks.map((item) => (item.id === updatedTask.id ? updatedTask : item));
          this.changeDetector.markForCheck();
        },
        error: () => {
          this.errorMessage = 'The task status could not be updated.';
          this.changeDetector.markForCheck();
        },
      });
  }

  deleteTask(taskId: string): void {
    if (!this.weddingId) return;
    this.taskService.deleteTask(this.weddingId, taskId).subscribe({
      next: () => {
        this.tasks = this.tasks.filter((task) => task.id !== taskId);
        this.confirmingDeleteId = null;
        this.changeDetector.markForCheck();
      },
      error: () => {
        this.errorMessage = 'The task could not be deleted.';
        this.confirmingDeleteId = null;
        this.changeDetector.markForCheck();
      },
    });
  }
}
