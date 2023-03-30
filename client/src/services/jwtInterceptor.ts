import axios from "axios";
import { CheckJWTAndSession, getLocalAccessToken } from "./tokenServices";

const jwtInterceptor = axios.create({
  baseURL: "https://localhost:7185",
  headers: {
    "Content-Type": "application/json",
  },
});

jwtInterceptor.interceptors.request.use(
  (config) => {
    const accessToken = getLocalAccessToken();
    if (accessToken && !CheckJWTAndSession()) {
      config.headers.set("Authorization", accessToken.token);
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);
