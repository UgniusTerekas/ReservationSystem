import jwtDecode from "jwt-decode";
import { JWTDeCode } from "../types/userAuth";

export function getLocalAccessToken() {
  // eslint-disable-next-line @typescript-eslint/consistent-type-assertions
  return JSON.parse(<string>localStorage.getItem("authToken"));
}

export function setLocalAccessoken(token: string) {
  localStorage.setItem("authToken", JSON.stringify(token));
}

export function removeLocalTokens() {
  localStorage.removeItem("authToken");
}

export async function CheckJWTAndSession() {
  const token = JSON.parse(localStorage.getItem("authToken") || "false");

  if (token === false) {
    return false;
  }

  const decoded: JWTDeCode = jwtDecode(token);

  // @ts-ignore
  if (decoded.exp < Date.now() / 1000) {
    localStorage.clear();
    localStorage.clear();
    return false;
  }
  return true;
}

export function pareRoleJWT(token: any): JWTDeCode {
  // eslint-disable-next-line @typescript-eslint/consistent-type-assertions
  if (token == null) return <JWTDeCode>{};
  const jwtBody = jwtDecode<object>(token.token);
  const userInfo: JWTDeCode = {
    UserId: "",
    Username: "",
    Email: "",
    Role: "",
    iat: -1,
    exp: -1,
    iss: "",
  };
  for (const [key, value] of Object.entries(jwtBody)) {
    const newKey = key.split("/").pop()?.toLowerCase();
    if (newKey === "role") {
      userInfo.Role = value as string;
    }
  }
  return userInfo;
}
