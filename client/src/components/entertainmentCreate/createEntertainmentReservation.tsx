import { Button, Divider, Form, Input, Space, TimePicker } from "antd";
import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { CreateReservationModel } from "../../types/reservation";

export const CreateEntertainmentReservation = () => {
  const format = "HH:mm";

  const [reservationModel, setReservationModel] =
    useState<CreateReservationModel>({
      entertainmentId: -1,
      startTime: "",
      endTime: "",
      breakTime: -1,
      maxCount: -1,
    });

  const handleChange = (name: string, value: string | number[]) => {
    setReservationModel((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = () => {};
  return (
    <React.Fragment>
      <Divider style={{ paddingInline: 30, borderColor: "black" }}>
        Rezervacijos Kurimas
      </Divider>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Form onFinish={handleSubmit}>
          <div
            style={{
              marginTop: 20,
              width: 1000,
              boxShadow: "0 0 10px 0 rgba(0, 0, 0, 0.1)",
              padding: 10,
              display: "flex",
              justifyContent: "center",
              borderRadius: "10px",
              backgroundColor: "#f2f2f2",
              border: "1px solid #e8e8e8",
              textAlign: "end",
            }}
          >
            <Space size={"large"}>
              <Space direction="vertical">
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Pradžios laikas:</p>
                  <TimePicker format={format} style={{ width: 260 }} />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Pabaigos laikas:</p>
                  <TimePicker format={format} style={{ width: 260 }} />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Rezervacijos intervalas:</p>
                  <Input
                    placeholder="Kas 15, kas 30, kas 1 valandą"
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>
                    Maksimalus rezervacijų skaičius:
                  </p>
                  <Input style={{ width: 260 }} />
                </Space>
                <Space
                  wrap
                  direction="horizontal"
                  style={{
                    width: "100%",
                    display: "flex",
                    justifyContent: "center",
                  }}
                >
                  <Button htmlType="submit" type="primary" size="large">
                    Išsaugoti
                  </Button>
                </Space>
              </Space>
            </Space>
          </div>
        </Form>
      </div>
    </React.Fragment>
  );
};
