import axios from "axios";
import { GetEntertainments } from "../types/entertainment";

const BACK_END_API = `https://localhost:7229`;

export const getEntertainments = async (signal: AbortSignal | undefined) => {
  const { data } = await axios.get<GetEntertainments[]>(
    BACK_END_API + "/api/Entertainment/entertainments",
    {
      signal,
    }
  );

  return data;
};
