import { Divider, Rate, Typography } from "antd";
import React from "react";

interface Props {
  rating: number;
  name: string | undefined;
}

export const EntertainmentRating = ({ name, rating }: Props) => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Pramogos Įvertinimas
      </Divider>
      <div className="entertainment-rating-container">
        <Typography.Title level={2} className="entertainment-rating-text">
          {name}
        </Typography.Title>
        <Rate
          allowHalf
          defaultValue={rating}
          disabled
          className="entertainment-rating-stars"
        />
        <Typography.Text className="entertainment-rating-number">
          {rating.toFixed(1)} / 5.0
        </Typography.Text>
      </div>
    </React.Fragment>
  );
};
