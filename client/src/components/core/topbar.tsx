import { Menu, MenuProps } from "antd";
import { Header } from "antd/es/layout/layout";
import { Link, useLocation } from "react-router-dom";
import { useRecoilState } from "recoil";
import { isValidToken } from "../../recoil/authStates";
import { removeLocalTokens } from "../../services/tokenServices";

type MenuItem = Required<MenuProps>["items"][number];

const getItem = (
  label: React.ReactNode,
  key: React.Key,
  className?: string,
  icon?: React.ReactNode,
  children?: MenuItem[],
  type?: string
): MenuItem => {
  return {
    key,
    className,
    icon,
    children,
    label,
    type,
  } as MenuItem;
};

export const TopBar = () => {
  const location = useLocation();

  const [, setTokenValidation] = useRecoilState(isValidToken);

  const logoutHandler = () => {
    setTokenValidation(false);
    removeLocalTokens();
  };

  const items: MenuProps["items"] = [
    getItem(<Link to={"/pagrindinis"}>Pagrindinis</Link>, "pagrindinis"),
    getItem(<Link to={"/pramogos"}>Pramogos</Link>, "pramogos"),
    getItem(
      <Link to={"/prisijungimas"} onClick={logoutHandler}>
        Atsijungti
      </Link>,
      "atsijungti",
      "atsijungtiLink"
    ),
  ];

  return (
    <Header
      style={{
        position: "sticky",
        top: 0,
        zIndex: 2,
        width: "100%",
      }}
    >
      <Menu
        mode="horizontal"
        theme="dark"
        className="top-menu-logged-in"
        items={items}
        defaultSelectedKeys={[location.pathname.substring(1)]}
      />
    </Header>
  );
};
