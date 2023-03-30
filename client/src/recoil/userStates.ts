import { atom } from "recoil";
import { JWTDeCode } from "../types/userAuth";

export const userStates = atom<JWTDeCode>({
  key: "userStates",
  default: undefined,
});
