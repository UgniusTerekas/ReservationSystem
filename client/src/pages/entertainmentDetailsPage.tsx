import React from "react";
import { EntertainmentGallery } from "../components/entertainmentDetails/entertainmentGallery";
import { EntertainmentAbout } from "../components/entertainmentDetails/entertainmentAbout";
import { EntertainmentRating } from "../components/entertainmentDetails/entertainmentRating";
import { EntertainmentAddress } from "../components/entertainmentDetails/entertainmentAddress";

export const EntertainmentDetailsPage = () => {
  return (
    <React.Fragment>
      <EntertainmentAbout />
      <EntertainmentGallery />
      <EntertainmentRating />
      <EntertainmentAddress />
    </React.Fragment>
  );
};
