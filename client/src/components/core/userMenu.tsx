import { useNavigate } from "react-router-dom";
import { removeLocalTokens } from "../../services/tokenServices";
import { useRecoilState } from "recoil";
import { isValidToken } from "../../recoil/authStates";
import { LogoutOutlined, UserOutlined } from "@ant-design/icons";
import { Avatar, Dropdown, MenuProps } from "antd";

export const UserMenu = () => {
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

  const items: MenuProps["items"] = [
    {
      key: "1",
      type: "group",
      children: [
        {
          key: "1-1",
          label: "Atsijungti",
          icon: <LogoutOutlined />,
          onClick: logoutHandler,
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
  ];
  return (
    <Dropdown menu={{ items }} trigger={["hover"]}>
      <Avatar size={40} icon={<UserOutlined />} />
    </Dropdown>
  );
};
