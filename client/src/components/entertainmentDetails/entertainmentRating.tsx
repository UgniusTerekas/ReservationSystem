import { Divider, Rate, Typography } from "antd";
import React from "react";

export const EntertainmentRating = () => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Pramogos Įvertinimas
      </Divider>
      <div className="entertainment-rating-container">
        <Typography.Title level={2} className="entertainment-rating-text">
          Pramogos Įvertinimas
        </Typography.Title>
        <Rate
          allowHalf
          defaultValue={2.5}
          disabled
          className="entertainment-rating-stars"
        />
        <Typography.Text className="entertainment-rating-number">
          {2.5} / 5.0
        </Typography.Text>
      </div>
    </React.Fragment>
  );
};
