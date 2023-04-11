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
import { QueryClient, QueryClientProvider } from "react-query";
import { EntertainmentListPage } from "./pages/entertainment/entertainmentListPage";
import { EntertainmentDetailsPage } from "./pages/entertainment/entertainmentDetailsPage";

const queryClient = new QueryClient({
  defaultOptions: { queries: { retry: false } },
});

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
    <QueryClientProvider client={queryClient}>
      <Routes>
        <Route path={"/"} element={<RootLayout />}>
          <Route path="/pagrindinis" element={<MainPage />} />
          <Route path="/pramogos" element={<EntertainmentListPage />} />
          <Route path="/pramoga/:id" element={<EntertainmentDetailsPage />} />
          <Route path="/prisijungimas" element={<LoginPage />} />
          <Route path="/registracija" element={<RegisterPage />} />
        </Route>
      </Routes>
    </QueryClientProvider>
  );
};
