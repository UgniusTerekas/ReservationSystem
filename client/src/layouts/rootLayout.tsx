import { Layout } from "antd";
import { Outlet } from "react-router-dom";
import { useRecoilState } from "recoil";
import { TopBar } from "../components/core/topbar";
import { TopBarLoggedOut } from "../components/core/topBarLoggedOut";
import { isValidToken } from "../recoil/authStates";

export const RootLayout = () => {
  const [validToken] = useRecoilState(isValidToken);

  return (
    <Layout style={{ minHeight: "100vh" }}>
      {validToken ? <TopBar /> : <TopBarLoggedOut />}
      <Layout>
        <Outlet />
      </Layout>
    </Layout>
  );
};
