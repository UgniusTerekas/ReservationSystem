import { Menu } from "antd";
import { Header } from "antd/es/layout/layout";
import { Link } from "react-router-dom";

export const TopBarLoggedOut = () => {
  return (
    <Header>
      <Menu mode="horizontal" theme="dark" className="top-menu">
        <Menu.Item key="1">
          <Link to="/">Pagrindinis</Link>
        </Menu.Item>
        <Menu.Item key="2">
          <Link to="/about">About</Link>
        </Menu.Item>
        <Menu.Item key="3">
          <Link to="/contact">Contact</Link>
        </Menu.Item>
        <Menu.Item key="5" className="registerLink">
          <Link to="/register">Register</Link>
        </Menu.Item>
        <Menu.Item key="6">
          <Link to="/login">Login</Link>
        </Menu.Item>
      </Menu>
    </Header>
  );
};
