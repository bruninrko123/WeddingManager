export interface Wedding {
  id: string;
  name: string;
  weddingDate: string | null;
  location: string;
}

export interface CreateWedding {
  name: string;
  weddingDate: string;
  location: string;
}
