import { UserInfo } from "../types/user";
import axios from "axios";
import { getLocalAccessToken } from "./tokenServices";

const BACK_END_API = `https://localhost:7229`;

export const getUserInformation = async (signal: AbortSignal | undefined) => {
  const token = getLocalAccessToken();
  axios.defaults.headers.get["Authorization"] = `Bearer ${token}`;
  const response = await axios.get<UserInfo>(
    BACK_END_API + "/api/User/userInfo"
  );

  return response;
};
