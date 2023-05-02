import { Routes, Route, useLocation, useNavigate } from "react-router-dom";
import { RegisterPage } from "./pages/auth/registerPage";
import { LoginPage } from "./pages/auth/loginPage";
import { MainPage } from "./pages/mainPage";
import { RootLayout } from "./layouts/rootLayout";
import { useEffect, useState } from "react";
import { useRecoilState } from "recoil";
import { isValidToken } from "./recoil/authStates";
import {
  CheckJWTAndSession,
  removeLocalTokens,
} from "./services/tokenServices";
import { QueryClient, QueryClientProvider } from "react-query";
import { EntertainmentListPage } from "./pages/entertainment/entertainmentListPage";
import { EntertainmentDetailsPage } from "./pages/entertainment/entertainmentDetailsPage";
import { EntertainmentCreate } from "./components/entertainmentCreate/entertainmentCreate";
import { CreateImagePage } from "./pages/images/createImagePage";
import { CityEntertainmentListPage } from "./pages/entertainment/cityEntertainmentListPage";
import { CategoryEntertainmentListPage } from "./pages/entertainment/categoryEntertainmentListPage";
import { UserPage } from "./pages/userPage";
import { AdminDashboardPage } from "./pages/adminDashboardPage";
import { CheckJWTIsAdmin } from "./services/authServices";
import { AdminRootLayout } from "./layouts/adminRootLayout";

const queryClient = new QueryClient({
  defaultOptions: { queries: { retry: false } },
});

export const App = () => {
  const [validToken, setTokenValidation] = useRecoilState(isValidToken);
  const [isAdmin, setIsAdmin] = useState(false);

  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();
      setIsAdmin(CheckJWTIsAdmin);

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
        {isAdmin && (
          <Route path={"/"} element={<AdminRootLayout />}>
            <Route path="/pagrindinis" element={<MainPage />} />
            <Route path="/pramogos" element={<EntertainmentListPage />} />
            <Route
              path="/pramogos/miestai/:cityId"
              element={<CityEntertainmentListPage />}
            />
            <Route
              path="/pramogos/kategorijos/:categoryId"
              element={<CategoryEntertainmentListPage />}
            />
            <Route path="/pramoga/:id" element={<EntertainmentDetailsPage />} />
            <Route
              path="/kurti/nuotrauka/:entertainmentId"
              element={<CreateImagePage />}
            />
            <Route path="/kurti/pramoga" element={<EntertainmentCreate />} />
            <Route path="/vartotojas" element={<UserPage />} />
            <Route path="/administratorius" element={<AdminDashboardPage />} />
            <Route path="/prisijungimas" element={<LoginPage />} />
            <Route path="/registracija" element={<RegisterPage />} />
          </Route>
        )}
        <Route path={"/"} element={<RootLayout />}>
          <Route path="/pagrindinis" element={<MainPage />} />
          <Route path="/pramogos" element={<EntertainmentListPage />} />
          <Route
            path="/pramogos/miestai/:cityId"
            element={<CityEntertainmentListPage />}
          />
          <Route
            path="/pramogos/kategorijos/:categoryId"
            element={<CategoryEntertainmentListPage />}
          />
          <Route path="/pramoga/:id" element={<EntertainmentDetailsPage />} />
          <Route
            path="/kurti/nuotrauka/:entertainmentId"
            element={<CreateImagePage />}
          />
          <Route path="/kurti/pramoga" element={<EntertainmentCreate />} />
          <Route path="/vartotojas" element={<UserPage />} />
          <Route path="/administratorius" element={<AdminDashboardPage />} />
          <Route path="/prisijungimas" element={<LoginPage />} />
          <Route path="/registracija" element={<RegisterPage />} />
        </Route>
      </Routes>
    </QueryClientProvider>
  );
};
