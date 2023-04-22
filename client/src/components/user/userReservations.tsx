import {
  EditOutlined,
  CalendarOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import { Descriptions, Space, Button, Input, Modal } from "antd";
import React, { useState } from "react";
import { UserReservationsModel } from "../../types/reservation";
import { useGoogleLogin } from "@react-oauth/google";
import { GoogleEvent, Reminder, ReservationDate } from "../../types/google";
import timezone from "dayjs/plugin/timezone";
import utc from "dayjs/plugin/utc";
import dayjs from "dayjs";
import { postEventInGoogleCalendar } from "../../services/googleServices";

dayjs.extend(utc);
dayjs.extend(timezone);

interface Props {
  reservation: UserReservationsModel;
}

export const UserReservations = ({ reservation }: Props) => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const scopes = [
    "https://www.googleapis.com/auth/calendar.events",
    "https://www.googleapis.com/auth/calendar",
  ];

  const login = useGoogleLogin({
    onSuccess: (tokenResponse) => handleLoginSuccess(tokenResponse),
    scope: scopes.join(" "),
  });

  const handleLoginSuccess = async (response: any) => {
    const meetStart: ReservationDate = {
      dateTime: new Date(reservation.time).toISOString(),
      timeZone: dayjs.tz.guess(),
    };

    const meetEnd: ReservationDate = {
      dateTime: dayjs(reservation.time).add(1, "hour").toDate().toISOString(),
      timeZone: dayjs.tz.guess(),
    };

    const reminder: Reminder = {
      useDefault: false,
      overrides: [
        { method: "email", minutes: 60 },
        { method: "popup", minutes: 30 },
      ],
    };

    const event: GoogleEvent = {
      summary: "Rezervacija: " + reservation?.entertainmentName,
      location: "Kaunas, Saules gatve 55",
      start: meetStart,
      end: meetEnd,
      reminder: reminder,
    };

    await postEventInGoogleCalendar(response.access_token, event);
  };

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  return (
    <React.Fragment>
      <Descriptions column={2} bordered style={{ borderColor: "black" }}>
        <Descriptions.Item
          label="Vartotojo slapyvardis"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}></label>
        </Descriptions.Item>
        <Descriptions.Item
          label="Vartotojo El.paštas"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}>sdfsdfsd</label>
        </Descriptions.Item>
        <Descriptions.Item
          label="Vartotojo rolė"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}>sdfsdf</label>
        </Descriptions.Item>
        <Descriptions.Item
          label="Vartotojo būsena"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}>dfgdfgd</label>
        </Descriptions.Item>
        <Descriptions.Item
          label="Vartotojo registracijos data"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}>dfgdfgd</label>
        </Descriptions.Item>
      </Descriptions>
      <Space
        style={{
          display: "flex",
          justifyContent: "center",
          marginTop: 10,
        }}
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
            type="primary"
            onClick={() => login()}
            icon={<CalendarOutlined />}
            style={{ backgroundColor: "#6366F1" }}
          >
            Pridėti į kalendorių
          </Button>
          <Button size="large" icon={<DeleteOutlined />} type="primary" danger>
            Pašalinti
          </Button>
        </Space>
      </Space>
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
              <Input style={{ width: 260 }} />
            </Space>
            <Space wrap direction="horizontal">
              <p style={{ fontSize: 16, width: 150 }}>Vartotojo El.paštas:</p>
              <Input style={{ width: 260 }} />
            </Space>
          </Space>
        </Space>
      </Modal>
    </React.Fragment>
  );
};
