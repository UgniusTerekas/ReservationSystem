import axios from "axios";
import {
  CreateEntertainment,
  GetEntertainment,
  GetEntertainmentDetails,
} from "../types/entertainment";
import { getLocalAccessToken } from "./tokenServices";

const BACK_END_API = `https://localhost:7229`;

export const getEntertainments = async (signal: AbortSignal | undefined) => {
  const { data } = await axios.get<GetEntertainment[]>(
    BACK_END_API + "/api/Entertainment/entertainments",
    {
      signal,
    }
  );

  return data;
};

export const getCityEntertainments = async (
  signal: AbortSignal | undefined,
  cityId: Number
) => {
  const { data } = await axios.get<GetEntertainment[]>(
    BACK_END_API + "/api/Entertainment/cityFilteredEntertainments",
    {
      signal,
      params: {
        cityId,
      },
    }
  );

  return data;
};

export const getCategoryEntertainments = async (
  signal: AbortSignal | undefined,
  categoryId: number
) => {
  const { data } = await axios.get<GetEntertainment[]>(
    BACK_END_API + "/api/Entertainment/categoryFilteredEntertainments",
    {
      signal,
      params: {
        categoryId,
      },
    }
  );

  return data;
};

export const getEntertainmentDetails = async (
  signal: AbortSignal | undefined,
  id: number
) => {
  const { data } = await axios.get<GetEntertainmentDetails>(
    BACK_END_API + "/api/Entertainment/entertainmentDetails",
    {
      signal,
      params: {
        Id: id,
      },
    }
  );

  return data;
};

export const createEntertainment = async (
  createEntertainmentRequest: CreateEntertainment
) => {
  const token = getLocalAccessToken();
  axios.defaults.headers.post["Authorization"] = `Bearer ${token}`;
  const response = await axios.post<number>(
    BACK_END_API + "/api/Entertainment/Entertainment",
    createEntertainmentRequest
  );

  return response;
};
