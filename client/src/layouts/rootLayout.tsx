import { Layout } from "antd";
import { Outlet } from "react-router-dom";
import { useRecoilState } from "recoil";
import { TopBar } from "../components/core/topbar";
import { TopBarLoggedOut } from "../components/core/topBarLoggedOut";
import { isValidToken } from "../recoil/authStates";
import { Footer } from "antd/es/layout/layout";
import { CheckJWTIsAdmin } from "../services/authServices";
import { AdminTopBar } from "../components/core/adminTopBar";
import { useEffect, useState } from "react";

export const RootLayout = () => {
  const [validToken] = useRecoilState(isValidToken);
  const [isAdmin, setIsAdmin] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      const promise = await CheckJWTIsAdmin();
      setIsAdmin(promise);
    };

    fetchData();
  }, []);

  return (
    <Layout style={{ minHeight: "100vh" }}>
      {validToken && isAdmin ? (
        <AdminTopBar />
      ) : validToken ? (
        <TopBar />
      ) : (
        <TopBarLoggedOut />
      )}
      <Layout>
        <Outlet />
      </Layout>
      <Footer
        style={{
          textAlign: "center",
          backgroundColor: "#bfbfbf",
          marginTop: 30,
          fontWeight: "bold",
        }}
      >
        Reservation System ©2023 Created by UT
      </Footer>
    </Layout>
  );
};
