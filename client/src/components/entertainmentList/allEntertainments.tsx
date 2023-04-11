import { Button, Card, Divider, Rate, Image, Space } from "antd";
import Meta from "antd/es/card/Meta";
import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { GetEntertainments } from "../../types/entertainment";

interface Props {
  entertainmentList: GetEntertainments[];
}

export const AllEntertainments = ({ entertainmentList }: Props) => {
  const [noReviews, setNoReviews] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    entertainmentList.forEach((element) => {
      if (element.rating === 0) {
        setNoReviews(true);
      }
    });
  }, [entertainmentList]);

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
                    element.image ? element.image : "https://picsum.photos/200"
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
              {!noReviews && (
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
                      <div style={{ marginTop: 10, fontWeight: "bold" }}>
                        Price: {element.price}$
                      </div>
                    </div>
                  }
                />
              )}
              {noReviews && (
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
