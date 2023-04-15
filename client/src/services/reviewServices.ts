import axios from "axios";
import { CreateReview } from "../types/review";
import { getLocalAccessToken } from "./tokenServices";

const BACK_END_API = `https://localhost:7229`;

export const createReview = async (createReviewRequest: CreateReview) => {
  const token = getLocalAccessToken();
  axios.defaults.headers.post["Authorization"] = `Bearer ${token}`;
  const response = await axios.post<CreateReview>(
    BACK_END_API + "/api/Review/review",
    createReviewRequest
  );

  return response;
};
