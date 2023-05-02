import { LogoutOutlined, UserOutlined } from "@ant-design/icons";
import { MenuProps, Dropdown, Avatar } from "antd";
import { useNavigate } from "react-router-dom";
import { useRecoilState } from "recoil";
import { isValidToken } from "../../recoil/authStates";
import { removeLocalTokens } from "../../services/tokenServices";

export const AdminMenu = () => {
  const navigate = useNavigate();

  const [, setTokenValidation] = useRecoilState(isValidToken);

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
          key: "1-2",
          label: "Administratoriaus informacija",
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
