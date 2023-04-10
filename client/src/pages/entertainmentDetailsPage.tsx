import React from "react";
import { EntertainmentGallery } from "../components/entertainmentDetails/entertainmentGallery";
import { EntertainmentAbout } from "../components/entertainmentDetails/entertainmentAbout";
import { EntertainmentRating } from "../components/entertainmentDetails/entertainmentRating";
import { EntertainmentReview } from "../components/entertainmentDetails/entertainmentReview";
import { EntertainmentReviews } from "../components/entertainmentDetails/entertainmentReviews";
import { EntertainmentReservation } from "../components/entertainmentDetails/entertainmentReservation";

export const EntertainmentDetailsPage = () => {
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
