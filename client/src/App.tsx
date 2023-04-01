import { Routes, Route, useLocation, useNavigate } from "react-router-dom";
import { RegisterPage } from "./pages/auth/registerPage";
import { LoginPage } from "./pages/auth/loginPage";
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

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();

      if (!check) {
        removeLocalTokens();
      }

      setTokenValidation(check);
    };
    if (location.pathname === "/" || location.pathname === "") {
      navigate("/pagrindinis");
    }
    validateToken();
  }, [validToken, setTokenValidation]);

  return (
    <Routes>
      <Route path={"/"} element={<RootLayout />}>
        <Route path="/pagrindinis" element={<MainPage />} />
        <Route path="/prisijungimas" element={<LoginPage />} />
        <Route path="/registracija" element={<RegisterPage />} />
      </Route>
    </Routes>
  );
};
