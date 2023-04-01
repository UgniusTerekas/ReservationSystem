import axios from "axios";
import { GetCitiesList } from "../types/city";

const BACK_END_API = `https://localhost:7229`;

export const getCities = async (signal: AbortSignal | undefined) => {
  const { data } = await axios.get<GetCitiesList[]>(
    BACK_END_API + "/api/City/getCities",
    {
      signal,
    }
  );

  return data;
};
