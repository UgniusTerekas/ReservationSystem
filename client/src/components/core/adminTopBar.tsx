import { MenuProps, Menu } from "antd";
import { Header } from "antd/es/layout/layout";
import { useLocation, Link } from "react-router-dom";
import { AdminMenu } from "./adminMenu";

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

export const AdminTopBar = () => {
  const location = useLocation();

  const items: MenuProps["items"] = [
    getItem(<Link to={"/pagrindinis"}>Pagrindinis</Link>, "pagrindinis"),
    getItem(<Link to={"/pramogos"}>Pramogos</Link>, "pramogos"),
    getItem(<AdminMenu />, "atsijungti", "atsijungtiLink"),
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
