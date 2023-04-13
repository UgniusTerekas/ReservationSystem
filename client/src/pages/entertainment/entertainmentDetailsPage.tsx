import React, { useState } from "react";
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

export const EntertainmentDetailsPage = () => {
  const { id } = useParams();

  const [entertainmentDetails, setEntertainmentDetails] =
    useState<GetEntertainmentDetails>();

  const query = useQuery({
    queryKey: [`entertainmentDetails/${id || ""}`],
    queryFn: ({ signal }) => {
      return getEntertainmentDetails(signal, Number(id));
    },
    onSuccess: (data) => {
      setEntertainmentDetails(data);
    },
  });

  return (
    <React.Fragment>
      <Skeleton active loading={query.isLoading}>
        <EntertainmentAbout
          name={entertainmentDetails?.name}
          description={entertainmentDetails?.description}
        />
        {entertainmentDetails?.gallery.length !== 0 && <EntertainmentGallery />}
        <EntertainmentRating />
        {entertainmentDetails?.reviews.length !== 0 && <EntertainmentReviews />}
        <EntertainmentReview />
        <EntertainmentReservation />
      </Skeleton>
    </React.Fragment>
  );
};
