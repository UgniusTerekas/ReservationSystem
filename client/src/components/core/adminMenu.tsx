import { useNavigate } from "react-router-dom";
import { removeLocalTokens } from "../../services/tokenServices";
import { useRecoilState } from "recoil";
import { isValidToken } from "../../recoil/authStates";
import { LogoutOutlined, UserOutlined } from "@ant-design/icons";
import { Avatar, Dropdown, MenuProps } from "antd";

export const AdminMenu = () => {
  const navigate = useNavigate();

  const [isTokenValid, setTokenValidation] = useRecoilState(isValidToken);

  const logoutHandler = () => {
    setTokenValidation(false);
    removeLocalTokens();
    navigate("/prisijungimas");
  };

  const createEntertainmentHandler = () => {
    navigate("/kurti/pramoga");
  };

  const customerInfoHandler = () => {
    navigate("/vartotojas");
  };

  const adminInfoHandler = () => {
    navigate("/administratorius");
  };

  const items: MenuProps["items"] = [
    {
      key: "1",
      type: "group",
      children: [
        {
          key: "1-1",
          label: "Administravimo pultas",
          onClick: adminInfoHandler,
        },
      ],
    },
    {
      key: "2",
      type: "group",
      children: [
        {
          key: "1-2",
          label: "Kurti pramogą",
          onClick: createEntertainmentHandler,
        },
      ],
    },
    {
      key: "3",
      type: "group",
      children: [
        {
          key: "1-3",
          label: "Atsijungti",
          icon: <LogoutOutlined />,
          onClick: logoutHandler,
        },
      ],
    },
  ];
  return (
    <Dropdown menu={{ items }} trigger={["hover"]}>
      <Avatar size={40} icon={<UserOutlined />} />
    </Dropdown>
  );
};
