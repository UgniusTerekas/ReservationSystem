import axios from "axios";
import { GetCategoriesList } from "../types/category";

const BACK_END_API = `https://localhost:7229`;

export const getCategories = async (signal: AbortSignal | undefined) => {
  const { data } = await axios.get<GetCategoriesList[]>(
    BACK_END_API + "/api/Category/getCategories",
    {
      signal,
    }
  );

  return data;
};
