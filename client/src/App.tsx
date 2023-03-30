import { Routes, Route } from "react-router-dom";
import { RegisterPage } from "./pages/auth/registerPage";
import { LoginPage } from "./pages/loginPage";
import { MainPage } from "./pages/mainPage";
import { RootLayout } from "./layouts/rootLayout";
import { useEffect } from "react";
import { useRecoilState } from "recoil";
import { isValidToken } from "./recoil/authStates";
import {
  CheckJWTAndSession,
  removeLocalTokens,
} from "./services/tokenServices";

export const App = () => {
  const [validToken, setTokenValidation] = useRecoilState(isValidToken);

  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();

      if (!check) {
        removeLocalTokens();
      }

      setTokenValidation(check);
    };

    validateToken();
  }, [validToken, setTokenValidation]);

  return (
    <Routes>
      <Route path={"/"} element={<RootLayout />}>
        <Route path="/" element={<MainPage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Route>
    </Routes>
  );
};
