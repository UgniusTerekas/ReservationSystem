import { Button, Card, Divider, Rate, Image, Space } from "antd";
import Meta from "antd/es/card/Meta";
import React from "react";
import { useNavigate } from "react-router-dom";

export const AllEntertainments = () => {
  const navigate = useNavigate();
  const buttonHandler = () => {
    navigate("/pramoga");
  };
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Pramogų Sąrašas
      </Divider>
      <Space direction="vertical" style={{ paddingInline: 30 }}>
        <Space direction="horizontal">
          <Card
            hoverable
            style={{ width: 240 }}
            cover={
              <Image
                src="https://picsum.photos/200"
                style={{ height: "200px", objectFit: "cover" }}
              />
            }
            actions={[
              <Button onClick={buttonHandler} type="primary" shape="round">
                Rezervuotis laiką
              </Button>,
            ]}
          >
            <Meta
              title="Product Name"
              description={
                <div>
                  <Rate disabled defaultValue={4} />
                  <span style={{ marginLeft: 10 }}>4.0</span>
                  <div style={{ marginTop: 10 }}>Price: $100</div>
                </div>
              }
            />
          </Card>
        </Space>
      </Space>
    </React.Fragment>
  );
};
