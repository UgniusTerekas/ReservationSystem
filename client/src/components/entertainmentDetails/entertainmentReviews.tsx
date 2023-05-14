import { Button, Divider, Rate, Typography } from "antd";
import { GetReviews } from "../../types/review";
import React, { useState } from "react";
import { GetUserName } from "../../services/authServices";
import { deleteReview } from "../../services/reviewServices";

const { Title, Paragraph } = Typography;

interface Props {
  reviews: GetReviews[] | undefined;
}

export const EntertainmentReviews = ({ reviews }: Props) => {
  const userName = GetUserName();
  const [isLoading, setIsLoading] = useState(false);

  const handleDelete = async (reviewId: number) => {
    setIsLoading(true);
    await deleteReview(reviewId);
    setIsLoading(false);
    window.location.reload();
  };

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        {" "}
        Atsiliepimai
      </Divider>
      {reviews?.map((review) => (
        <div
          key={review.id}
          style={{
            backgroundColor: "white",
            marginInline: "30px",
            marginTop: 10,
          }}
        >
          <>
            <Title style={{ paddingInline: 10 }} level={4}>
              {review.username}
            </Title>
            <Rate
              style={{ paddingInline: 10 }}
              disabled
              defaultValue={review.rating}
            />
            {review.rating} / 5
            <Paragraph style={{ paddingInline: 10, paddingTop: 5 }}>
              {review.description} <br />
            </Paragraph>
            {userName && review.username === userName && (
              <Button
                type="primary"
                danger
                loading={isLoading}
                onClick={() => handleDelete(review.id)}
              >
                Ištrinti
              </Button>
            )}
          </>
        </div>
      ))}
    </React.Fragment>
  );
};
