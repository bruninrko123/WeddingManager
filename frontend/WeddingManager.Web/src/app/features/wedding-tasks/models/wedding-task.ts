export interface WeddingTask {
  id: string;
  weddingId: string;
  title: string;
  description: string | null;
  dueDate: string | null;
  isCompleted: boolean;
}

export interface CreateWeddingTask {
  title: string;
  description: string | null;
  dueDate: string | null;
}

export interface UpdateWeddingTask {
  title: string;
  description: string | null;
  dueDate: string | null;
  isCompleted: boolean;
}
