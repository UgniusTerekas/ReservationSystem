import { atom } from "recoil";

export const authTokenAtom = atom({
  key: "authToken",
  default: false,
});

export const isAdmin = atom({
  key: "isAdmin",
  default: false,
});

export const isValidToken = atom({
  key: "isValidToken",
  default: false,
});
