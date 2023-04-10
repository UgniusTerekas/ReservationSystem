import { Divider, Rate, Typography } from "antd";
import React, { useState } from "react";

const { Title, Paragraph } = Typography;

export const EntertainmentReviews = () => {
  const [reviewer, setReviewer] = useState("John Doe");
  const [review, setReview] = useState(
    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eget semper nunc, quis rhoncus metus. Sed vitae bibendum nibh, vitae ultricies nisi. Aenean in elit ante."
  );
  const [rating, setRating] = useState(3);
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        {" "}
        Atsiliepimai
      </Divider>
      <div style={{ backgroundColor: "white", marginInline: "30px" }}>
        <Title style={{ paddingInline: 10 }} level={4}>
          {reviewer}
        </Title>
        <Rate style={{ paddingInline: 10 }} disabled defaultValue={rating} />
        {rating} / 5
        <Paragraph style={{ paddingInline: 10, paddingTop: 5 }}>
          {review} <br />
        </Paragraph>
      </div>
    </React.Fragment>
  );
};
