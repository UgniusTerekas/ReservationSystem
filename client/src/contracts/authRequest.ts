import { LoginCredentials } from "./../types/userAuth";
import { RegisterCredentials } from "../types/userAuth";

export interface RegisterRequest {
  userRegisterDto: RegisterCredentials;
}

export interface LoginRequest {
  userLoginRequest: LoginCredentials;
}

export interface LoginResponse {
  tokenJWT: string;
}
