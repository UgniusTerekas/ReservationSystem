import React, { useEffect, useState } from "react";
import { EntertainmentGallery } from "../../components/entertainmentDetails/entertainmentGallery";
import { EntertainmentAbout } from "../../components/entertainmentDetails/entertainmentAbout";
import { EntertainmentRating } from "../../components/entertainmentDetails/entertainmentRating";
import { EntertainmentReview } from "../../components/entertainmentDetails/entertainmentReview";
import { EntertainmentReviews } from "../../components/entertainmentDetails/entertainmentReviews";
import { EntertainmentReservation } from "../../components/entertainmentDetails/entertainmentReservation";
import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { getEntertainmentDetails } from "../../services/entertainment";
import { Skeleton } from "antd";
import { GetEntertainmentDetails } from "../../types/entertainment";
import { CheckJWTAndSession } from "../../services/tokenServices";

export const EntertainmentDetailsPage = () => {
  const { id } = useParams();

  const [entertainmentDetails, setEntertainmentDetails] =
    useState<GetEntertainmentDetails>();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const ratingAverage = entertainmentDetails?.reviews
    ? entertainmentDetails?.reviews?.reduce(
        (total, review) => total + review.rating,
        0
      ) / (entertainmentDetails?.reviews?.length || 1)
    : 0;

  const query = useQuery({
    queryKey: [`entertainmentDetails/${id || ""}`],
    queryFn: ({ signal }) => {
      return getEntertainmentDetails(signal, Number(id));
    },
    onSuccess: (data) => {
      setEntertainmentDetails(data);
    },
  });

  useEffect(() => {
    const validateToken = async () => {
      const check = await CheckJWTAndSession();

      if (!check) {
        setIsLoggedIn(check);
      }

      setIsLoggedIn(check);
    };
    validateToken();
  }, []);

  return (
    <React.Fragment>
      <Skeleton active loading={query.isLoading}>
        <EntertainmentAbout
          name={entertainmentDetails?.name}
          description={entertainmentDetails?.description}
        />
        {entertainmentDetails?.gallery.length !== 0 && <EntertainmentGallery />}
        {entertainmentDetails?.reviews.length !== 0 && (
          <EntertainmentRating
            name={entertainmentDetails?.name}
            rating={ratingAverage}
          />
        )}
        {entertainmentDetails?.reviews.length !== 0 && (
          <EntertainmentReviews reviews={entertainmentDetails?.reviews} />
        )}
        {isLoggedIn && <EntertainmentReview id={Number(id)} />}
        <EntertainmentReservation />
      </Skeleton>
    </React.Fragment>
  );
};
