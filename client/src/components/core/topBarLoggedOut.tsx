import { Menu, MenuProps } from "antd";
import { Header } from "antd/es/layout/layout";
import { Link, useLocation } from "react-router-dom";

type MenuItem = Required<MenuProps>["items"][number];

const getItem = (
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
  type?: string
): MenuItem => {
  return {
    key,
    icon,
    children,
    label,
    type,
  } as MenuItem;
};

export const TopBarLoggedOut = () => {
  const location = useLocation();

  const items: MenuProps["items"] = [
    getItem(<Link to={"/pagrindinis"}>Pagrindinis</Link>, "pagrindinis"),
    getItem(<Link to={"/visosPramogos"}>Visos Pramogos</Link>, "visosPramogos"),
    getItem(
      <Link className="registerLink" to={"/registracija"}>
        Registracija
      </Link>,
      "registracija"
    ),
    getItem(<Link to={"/prisijungimas"}>Prisijungimas</Link>, "prisijungimas"),
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
        className="top-menu"
        items={items}
        defaultSelectedKeys={[location.pathname.substring(1)]}
      />
    </Header>
  );
};
