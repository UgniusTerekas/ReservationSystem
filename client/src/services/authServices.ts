import { LoginRequest, LoginResponse } from "./../contracts/authRequest";
import axios from "axios";
import { RegisterRequest } from "../contracts/authRequest";
import { JWTDeCode } from "../types/userAuth";
import jwt_decode from "jwt-decode";

const BACK_END_API = `https://localhost:7229`;

export const postRegisterRequest = async (userRegisterDto: RegisterRequest) => {
  const { data } = await axios.post<boolean>(
    BACK_END_API + "/api/Auth/register",
    userRegisterDto
  );

  return data;
};

export const postLoginRequest = async (userLoginDto: LoginRequest) => {
  const { data } = await axios.post<LoginResponse>(
    BACK_END_API + "/api/Auth/login",
    userLoginDto
  );

  return data.tokenJWT;
};

export function CheckJWTIsAdmin() {
  const authToken = JSON.parse(localStorage.getItem("authToken") || "null");

  if (authToken === null || authToken === undefined) {
    return false;
  }

  const decoded: JWTDeCode = jwt_decode(authToken);

  if (decoded.exp < Date.now() / 1000) {
    localStorage.clear();
    return false;
  }

  if (decoded.RoleId === "1") {
    return true;
  }
  return false;
}

export function GetUserName() {
  const authToken = JSON.parse(localStorage.getItem("authToken") || "null");

  if (authToken === null || authToken === undefined) {
    return undefined;
  }

  const decoded: JWTDeCode = jwt_decode(authToken);

  if (decoded.exp < Date.now() / 1000) {
    localStorage.clear();
    return undefined;
  }

  return decoded.Username;
}
