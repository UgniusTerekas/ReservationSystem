import { Layout } from "antd";
import { Outlet } from "react-router-dom";
import { useRecoilState } from "recoil";
import { TopBar } from "../components/core/topbar";
import { TopBarLoggedOut } from "../components/core/topBarLoggedOut";
import { isValidToken } from "../recoil/authStates";
import { Footer } from "antd/es/layout/layout";

export const RootLayout = () => {
  const [validToken] = useRecoilState(isValidToken);

  return (
    <Layout style={{ minHeight: "100vh" }}>
      {validToken ? <TopBar /> : <TopBarLoggedOut />}
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
