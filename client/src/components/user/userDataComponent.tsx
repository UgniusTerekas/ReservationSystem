import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { Button, Descriptions, Divider, Input, Modal, Space } from "antd";
import React, { useEffect, useState } from "react";
import { UserInfo } from "../../types/user";
import { useQuery } from "react-query";
import { getUserInformation } from "../../services/userServices";

export const UserDataComponent = () => {
  const [userInfo, setUserInfo] = useState<UserInfo>();
  const [role, setRole] = useState<string>("");
  const [state, setState] = useState<string>("");
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  useQuery({
    queryKey: ["userInfo"],
    queryFn: ({ signal }) => getUserInformation(signal),
    onSuccess: (data) => {
      setUserInfo(data.data);
    },
  });

  const dateString = userInfo?.registrationDate;

  const formattedDate = new Date(dateString!).toLocaleString("en-GB", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
  });

  useEffect(() => {
    if (userInfo?.role === "RegisteredUser") {
      setRole("Prisiregistravęs vartotojas");
    }

    if (userInfo?.state === "Active") {
      setState("Aktyvus");
    }
  }, []);

  return (
    <React.Fragment>
      <Divider style={{ paddingInline: 30, borderColor: "black" }}>
        Vartotojo Duomenys
      </Divider>
      <div
        style={{
          width: "100%",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <div
          style={{
            borderStyle: "solid",
            borderWidth: "1px",
            margin: "15px",
            padding: "15px",
            borderRadius: "5px",
            borderColor: "black",
          }}
        >
          <Descriptions column={2} bordered style={{ borderColor: "black" }}>
            <Descriptions.Item
              label="Vartotojo slapyvardis"
              labelStyle={{ fontWeight: "bold" }}
            >
              <label style={{ fontWeight: "bold" }}>{userInfo?.username}</label>
            </Descriptions.Item>
            <Descriptions.Item
              label="Vartotojo El.paštas"
              labelStyle={{ fontWeight: "bold" }}
            >
              <label style={{ fontWeight: "bold" }}>{userInfo?.email}</label>
            </Descriptions.Item>
            <Descriptions.Item
              label="Vartotojo rolė"
              labelStyle={{ fontWeight: "bold" }}
            >
              <label style={{ fontWeight: "bold" }}>{role}</label>
            </Descriptions.Item>
            <Descriptions.Item
              label="Vartotojo būsena"
              labelStyle={{ fontWeight: "bold" }}
            >
              <label style={{ fontWeight: "bold" }}>{state}</label>
            </Descriptions.Item>
            <Descriptions.Item
              label="Vartotojo registracijos data"
              labelStyle={{ fontWeight: "bold" }}
            >
              <label style={{ fontWeight: "bold" }}>{formattedDate}</label>
            </Descriptions.Item>
          </Descriptions>
          <Space
            style={{ display: "flex", justifyContent: "center", marginTop: 10 }}
          >
            <Space>
              <Button
                onClick={() => setIsModalOpen(true)}
                icon={<EditOutlined />}
                type="primary"
                size="large"
              >
                Redaguoti
              </Button>
              <Button
                size="large"
                icon={<DeleteOutlined />}
                type="primary"
                danger
              >
                Pašalinti
              </Button>
            </Space>
          </Space>
        </div>
      </div>
      <Modal
        title="Vartotojo Informacijos Redagavimas"
        centered
        open={isModalOpen}
        onOk={handleOk}
        onCancel={handleCancel}
        footer={
          <div>
            <Button onClick={handleCancel}>Atšaukti</Button>
            <Button type="primary" onClick={handleOk}>
              Išsaugoti
            </Button>
          </div>
        }
      >
        <Space size={"large"}>
          <Space direction="vertical">
            <Space wrap direction="horizontal">
              <p style={{ fontSize: 16 }}>Vartotojo slapyvardis:</p>
              <Input value={userInfo?.username} style={{ width: 260 }} />
            </Space>
            <Space wrap direction="horizontal">
              <p style={{ fontSize: 16, width: 150 }}>Vartotojo El.paštas:</p>
              <Input value={userInfo?.email} style={{ width: 260 }} />
            </Space>
          </Space>
        </Space>
      </Modal>
    </React.Fragment>
  );
};
