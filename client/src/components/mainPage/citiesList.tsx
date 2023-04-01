import { Card, Divider, Space } from "antd";
import React from "react";
import { Link } from "react-router-dom";

const cardData = [
  {
    id: 1,
    title: "Card 1",
    image: "https://picsum.photos/id/1/800/600",
    link: "/card1",
  },
  {
    id: 2,
    title: "Card 2",
    image: "https://picsum.photos/id/2/800/600",
    link: "/card2",
  },
  {
    id: 3,
    title: "Card 3",
    image: "https://picsum.photos/id/3/800/600",
    link: "/card3",
  },
  {
    id: 4,
    title: "Card 4",
    image: "https://picsum.photos/id/4/800/600",
    link: "/card4",
  },
  {
    id: 5,
    title: "Card 5",
    image: "https://picsum.photos/id/5/800/600",
    link: "/card5",
  },
  {
    id: 6,
    title: "Card 6",
    image: "https://picsum.photos/id/6/800/600",
    link: "/card6",
  },
];

export const CitiesList = () => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 110 }}>
        <h1>Paieška pagal miestą</h1>
      </Divider>
      <Space
        size={16}
        wrap
        style={{ justifyContent: "center", display: "flex", padding: "0 30px" }}
      >
        {cardData.map((card) => (
          <Link to={card.link} key={card.id}>
            <Card
              hoverable
              cover={
                <img
                  src={card.image}
                  alt={card.title}
                  className="cities-card-image"
                />
              }
              className="cities-card"
            >
              <h3 className="cities-card-title">{card.title}</h3>
            </Card>
          </Link>
        ))}
      </Space>
    </React.Fragment>
  );
};
