import axios from "axios";
import {
  AdminReservationsModel,
  CreateReservationModel,
  CreateUserReservationModel,
  EntertainmentReservationsModel,
  ReservationFillDataModel,
  UserReservationsModel,
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

export const createUserReservation = async (
  createUserReservationModel: CreateUserReservationModel
) => {
  const token = getLocalAccessToken();
  axios.defaults.headers.patch["Authorization"] = `Bearer ${token}`;
  const { data } = await axios.patch<CreateReservationModel>(
    BACK_END_API + "/api/Reservation/createUserReservation",
    createUserReservationModel
  );

  return data;
};

export const getUserReservations = async (signal: AbortSignal | undefined) => {
  const token = getLocalAccessToken();
  axios.defaults.headers.get["Authorization"] = `Bearer ${token}`;
  const { data } = await axios.get<UserReservationsModel[]>(
    BACK_END_API + "/api/Reservation/userReservations",
    {
      signal,
    }
  );

  return data;
};

export const getEntertainmentReservations = async (
  signal: AbortSignal | undefined,
  entertainmentId: number,
  date: string
) => {
  const { data } = await axios.get<EntertainmentReservationsModel[]>(
    BACK_END_API + "/api/Reservation/entertainmentsReservations",
    {
      signal,
      params: {
        entertainmentId,
        date,
      },
    }
  );

  return data;
};

export const getAdminReservations = async (signal: AbortSignal | undefined) => {
  const token = getLocalAccessToken();
  axios.defaults.headers.get["Authorization"] = `Bearer ${token}`;
  const { data } = await axios.get<AdminReservationsModel[]>(
    BACK_END_API + "/api/Reservation/adminDashboard/reservations",
    {
      signal,
    }
  );

  return data;
};

export const deleteUserReservation = async (reservationId: number) => {
  const { data } = await axios.delete(
    BACK_END_API + "/api/Reservation/userReservations",
    {
      params: {
        reservationId,
      },
    }
  );

  return data;
};
