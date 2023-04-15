export interface GetReviews {
  id: number;
  username: string;
  rating: number;
  description: string;
}

export interface CreateReview {
  entertainmentId: number;
  rating: number;
  description: string;
}
