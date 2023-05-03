import {
  EditOutlined,
  CalendarOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import { Descriptions, Space, Button, Input, Modal, message } from "antd";
import React, { useEffect, useState } from "react";
import { UserReservationsModel } from "../../types/reservation";
import { useGoogleLogin } from "@react-oauth/google";
import { GoogleEvent, Reminder, ReservationDate } from "../../types/google";
import timezone from "dayjs/plugin/timezone";
import utc from "dayjs/plugin/utc";
import dayjs from "dayjs";
import { postEventInGoogleCalendar } from "../../services/googleServices";
import { deleteUserReservation } from "../../services/reservationServices";

dayjs.extend(utc);
dayjs.extend(timezone);

interface Props {
  reservation: UserReservationsModel;
}

export const UserReservations = ({ reservation }: Props) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [messageApi, contextHolder] = message.useMessage();
  const [isLoading, setIsLoading] = useState(false);
  const [isDisabled, setIsDisabled] = useState(false);

  const success = (text: string) => {
    messageApi.open({
      type: "success",
      content: text,
    });
  };

  const error = () => {
    messageApi.open({
      type: "error",
      content: "Nepavyko prisijungti!",
    });
  };

  const scopes = [
    "https://www.googleapis.com/auth/calendar.events",
    "https://www.googleapis.com/auth/calendar",
  ];

  const login = useGoogleLogin({
    onSuccess: (tokenResponse) => handleLoginSuccess(tokenResponse),
    onError: error,
    scope: scopes.join(" "),
  });

  const handleLoginSuccess = async (response: any) => {
    const dateString = reservation.duration;
    const [dateComponent, timeComponent] = dateString.split(" ");
    const [hoursString, minutesString] = timeComponent.split(":");
    const hours = parseInt(hoursString);
    const minutes = parseInt(minutesString);

    const meetStart: ReservationDate = {
      dateTime: new Date(reservation.time).toISOString(),
      timeZone: dayjs.tz.guess(),
    };

    const meetEnd: ReservationDate = {
      dateTime: dayjs(reservation.time)
        .add(hours, "hour")
        .add(minutes, "minutes")
        .toDate()
        .toISOString(),
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
      location: reservation.address,
      start: meetStart,
      end: meetEnd,
      reminder: reminder,
    };

    await postEventInGoogleCalendar(response.access_token, event);
    success("Sėkmingai pridėta į kalendorių!");
  };

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const durationDate = reservation?.duration;

  const formattedDuration = new Date(durationDate!).toLocaleString("en-GB", {
    hour: "2-digit",
    minute: "2-digit",
  });

  const reservationDate = reservation?.date;

  const formattedReservationDate = new Date(reservationDate!).toLocaleString(
    "en-GB",
    {
      year: "numeric",
      month: "2-digit",
      day: "2-digit",
    }
  );

  const reservationTime = reservation?.time;

  const formattedReservationTime = new Date(reservationTime!).toLocaleString(
    "en-GB",
    {
      hour: "2-digit",
      minute: "2-digit",
    }
  );

  const deleteHandler = async () => {
    setIsLoading(true);
    await deleteUserReservation(reservation.reservationId);
    success("Sėkmingai pašalinta!");
    setIsLoading(false);
    window.location.reload();
  };

  useEffect(() => {
    // Get the current date/time
    const now = dayjs();

    // Parse the reservation date/time string
    const reservationDateTime = dayjs(reservation.date, "M/D/YYYY h:mm:ss A");

    // Calculate the difference in days
    const daysDiff = reservationDateTime.diff(now, "day");

    // Check if the difference is 3 or more
    setIsDisabled(daysDiff < 3);
  }, []);

  return (
    <React.Fragment>
      {contextHolder}
      <Descriptions column={2} bordered style={{ borderColor: "black" }}>
        <Descriptions.Item
          label="Pramogos Pavadinimas"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}>
            {reservation.entertainmentName}
          </label>
        </Descriptions.Item>
        <Descriptions.Item
          label="Rezervacijos ID"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}>
            {reservation.reservationId}
          </label>
        </Descriptions.Item>
        <Descriptions.Item label="Kaina" labelStyle={{ fontWeight: "bold" }}>
          <label style={{ fontWeight: "bold" }}>{reservation.price}€</label>
        </Descriptions.Item>
        <Descriptions.Item label="Trukme" labelStyle={{ fontWeight: "bold" }}>
          <label style={{ fontWeight: "bold" }}>{formattedDuration} val</label>
        </Descriptions.Item>
        <Descriptions.Item
          label="Data ir laikas"
          labelStyle={{ fontWeight: "bold" }}
        >
          <label style={{ fontWeight: "bold" }}>
            {formattedReservationDate} {formattedReservationTime} val
          </label>
        </Descriptions.Item>
        <Descriptions.Item label="Adresas" labelStyle={{ fontWeight: "bold" }}>
          <label style={{ fontWeight: "bold" }}>{reservation.address}</label>
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
            size="large"
            type="primary"
            onClick={() => login()}
            icon={<CalendarOutlined />}
            style={{ backgroundColor: "#6366F1" }}
          >
            Pridėti į kalendorių
          </Button>
          <Button
            onClick={deleteHandler}
            size="large"
            icon={<DeleteOutlined />}
            type="primary"
            danger
            loading={isLoading}
            disabled={isDisabled}
          >
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
