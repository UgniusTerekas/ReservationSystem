import {
  EnvironmentOutlined,
  MailOutlined,
  PhoneOutlined,
} from "@ant-design/icons";
import { Card, Col, Divider, Row, Space, Typography } from "antd";
import Title from "antd/es/typography/Title";
import React from "react";

const { Text } = Typography;

interface Props {
  name: string | undefined;
  description: string | undefined;
  email: string | undefined;
  address: string | undefined;
  phoneNumber: string | undefined;
}

export const EntertainmentAbout = ({
  name,
  description,
  email,
  address,
  phoneNumber,
}: Props) => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Apie Pramogą
      </Divider>
      <div style={{ margin: "30px" }}>
        <Card title={name}>
          <p style={{ fontSize: 16 }}>{description}</p>
          <Divider />
          <Row justify="space-between" align="middle">
            <Col span={24} lg={8} style={{ marginBottom: 10 }}>
              <Title level={5} style={{ fontWeight: "bold" }}>
                <PhoneOutlined style={{ marginRight: 10 }} />
                <a href={`tel:${phoneNumber}`} style={{ color: "inherit" }}>
                  {phoneNumber}
                </a>
              </Title>
            </Col>
            <Col span={24} lg={8} style={{ marginBottom: 10 }}>
              <Title level={5} style={{ fontWeight: "bold" }}>
                <MailOutlined style={{ marginRight: 10 }} />
                <a href={`mailto:${email}`} style={{ color: "inherit" }}>
                  {email}
                </a>
              </Title>
            </Col>
            <Col span={24} lg={8} style={{ marginBottom: 10 }}>
              <Title level={5} style={{ fontWeight: "bold" }}>
                <EnvironmentOutlined style={{ marginRight: 10 }} />
                <a
                  href={`https://www.google.com/maps/search/?api=1&query=${encodeURIComponent(
                    address!
                  )}`}
                  target="_blank"
                  rel="noopener noreferrer"
                  style={{ color: "inherit" }}
                >
                  <Text ellipsis={{ tooltip: address }}>{address}</Text>
                </a>
              </Title>
            </Col>
          </Row>
        </Card>
      </div>
    </React.Fragment>
  );
};
