import { Menu } from "antd";
import { Header } from "antd/es/layout/layout";
import { Link } from "react-router-dom";
import { useRecoilState } from "recoil";
import { isValidToken } from "../../recoil/authStates";
import { removeLocalTokens } from "../../services/tokenServices";

export const TopBar = () => {
  const [, setTokenValidation] = useRecoilState(isValidToken);

  const logoutHandler = () => {
    setTokenValidation(false);
    removeLocalTokens();
  };

  return (
    <Header className="header">
      <Menu mode="horizontal" theme="dark" className="top-menu-logged-in">
        <Menu.Item key="pagrindinis">
          <Link to="/">Pagrindinis</Link>
        </Menu.Item>
        <Menu.Item key="2" className="top-menu-logged-in">
          <Link to="/">Visos Pramogos</Link>
        </Menu.Item>
        <Menu.Item key="5" style={{ marginLeft: "auto" }}>
          <Link onClick={logoutHandler} to={"/login"}>
            Logout
          </Link>
        </Menu.Item>
      </Menu>
    </Header>
  );
};
