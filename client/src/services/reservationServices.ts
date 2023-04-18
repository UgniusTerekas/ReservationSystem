import axios from "axios";
import {
  CreateReservationModel,
  ReservationFillDataModel,
} from "../types/reservation";
import { getLocalAccessToken } from "./tokenServices";

const BACK_END_API = `https://localhost:7229`;

export const createEntertainmentReservation = async (
  createEntertainmentReservation: CreateReservationModel
) => {
  const token = getLocalAccessToken();
  axios.defaults.headers.post["Authorization"] = `Bearer ${token}`;
  const response = await axios.post<CreateReservationModel>(
    BACK_END_API + "/api/Reservation/createReservation",
    createEntertainmentReservation
  );

  return response;
};

export const getReservationFillData = async (
  signal: AbortSignal | undefined,
  entertainmentId: number
) => {
  const { data } = await axios.get<ReservationFillDataModel>(
    BACK_END_API + "/api/Reservation/reservations/fill/data",
    {
      signal,
      params: {
        entertainmentId,
      },
    }
  );

  return data;
};
