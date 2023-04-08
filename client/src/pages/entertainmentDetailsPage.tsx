import React from "react";
import { EntertainmentGallery } from "../components/entertainmentDetails/entertainmentGallery";
import { EntertainmentAbout } from "../components/entertainmentDetails/entertainmentAbout";

export const EntertainmentDetailsPage = () => {
  return (
    <React.Fragment>
      <EntertainmentAbout />
      <EntertainmentGallery />
    </React.Fragment>
  );
};
