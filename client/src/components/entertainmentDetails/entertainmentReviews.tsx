import { Divider, Rate, Typography } from "antd";
import { GetReviews } from "../../types/review";
import React from "react";

const { Title, Paragraph } = Typography;

interface Props {
  reviews: GetReviews[] | undefined;
}

export const EntertainmentReviews = ({ reviews }: Props) => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        {" "}
        Atsiliepimai
      </Divider>
      {reviews?.map((review) => (
        <div
          style={{
            backgroundColor: "white",
            marginInline: "30px",
            marginTop: 10,
          }}
        >
          <>
            <Title
              key={review.description}
              style={{ paddingInline: 10 }}
              level={4}
            >
              {review.username}
            </Title>
            <Rate
              key={review.id}
              style={{ paddingInline: 10 }}
              disabled
              defaultValue={review.rating}
            />
            {review.rating} / 5
            <Paragraph style={{ paddingInline: 10, paddingTop: 5 }}>
              {review.description} <br />
            </Paragraph>
          </>
        </div>
      ))}
    </React.Fragment>
  );
};
