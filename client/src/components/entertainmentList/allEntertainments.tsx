import { Button, Card, Divider, Rate, Image, Space } from "antd";
import Meta from "antd/es/card/Meta";
import React from "react";
import { Link } from "react-router-dom";
import { GetEntertainment } from "../../types/entertainment";

interface Props {
  entertainmentList: GetEntertainment[];
}

export const AllEntertainments = ({ entertainmentList }: Props) => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Pramogų Sąrašas
      </Divider>
      <Space direction="horizontal">
        {entertainmentList.map((element) => (
          <Space
            direction="horizontal"
            style={{ marginLeft: 30 }}
            key={element.id}
          >
            <Card
              className="entertainmentList"
              hoverable
              style={{ width: 240 }}
              cover={
                <Image
                  src={
                    element.image
                      ? element.image.imageLocation
                      : "https://picsum.photos/200"
                  }
                  style={{ height: "200px", objectFit: "cover" }}
                />
              }
              actions={[
                <Link to={`/pramoga/${element.id}`}>
                  <Button type="primary" shape="round">
                    Rezervuotis laiką
                  </Button>
                </Link>,
              ]}
            >
              {element.rating >= 0 && (
                <Meta
                  title={
                    <div style={{ whiteSpace: "normal" }}>{element.name}</div>
                  }
                  description={
                    <div>
                      <Rate disabled defaultValue={element.rating} />
                      <span style={{ marginLeft: 10 }}>
                        {element.rating.toFixed(1)}
                      </span>
                      <div
                        style={{
                          marginTop: 10,
                          fontWeight: "bold",
                          color: "black",
                        }}
                      >
                        Kaina: {element.price}€
                      </div>
                    </div>
                  }
                />
              )}
              {element.rating === undefined && (
                <Meta
                  title={
                    <div style={{ whiteSpace: "normal" }}>{element.name}</div>
                  }
                  description={
                    <div>
                      <span>Nėra įvertinimų</span>
                      <div style={{ marginTop: 10, fontWeight: "bold" }}>
                        Price: {element.price}$
                      </div>
                    </div>
                  }
                />
              )}
            </Card>
          </Space>
        ))}
      </Space>
    </React.Fragment>
  );
};
