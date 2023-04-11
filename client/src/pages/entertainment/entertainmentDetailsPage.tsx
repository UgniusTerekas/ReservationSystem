import React from "react";
import { EntertainmentGallery } from "../../components/entertainmentDetails/entertainmentGallery";
import { EntertainmentAbout } from "../../components/entertainmentDetails/entertainmentAbout";
import { EntertainmentRating } from "../../components/entertainmentDetails/entertainmentRating";
import { EntertainmentReview } from "../../components/entertainmentDetails/entertainmentReview";
import { EntertainmentReviews } from "../../components/entertainmentDetails/entertainmentReviews";
import { EntertainmentReservation } from "../../components/entertainmentDetails/entertainmentReservation";
import { useParams } from "react-router-dom";

export const EntertainmentDetailsPage = () => {
  const { id } = useParams();

  return (
    <React.Fragment>
      <EntertainmentAbout />
      <EntertainmentGallery />
      <EntertainmentRating />
      <EntertainmentReviews />
      <EntertainmentReview />
      <EntertainmentReservation />
    </React.Fragment>
  );
};
