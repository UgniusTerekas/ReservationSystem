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

export async function CheckJWTIsAdmin() {
  const token = JSON.parse(localStorage.getItem("token") || "false");

  if (token === false) {
    return false;
  }

  const decoded: JWTDeCode = jwt_decode(token.token);

  if (decoded.exp < Date.now() / 1000) {
    localStorage.clear();
    return false;
  }

  if (decoded.Role === "Admin") {
    return true;
  }
  return false;
}
