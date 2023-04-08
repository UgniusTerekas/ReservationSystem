import React from "react";
import { Filters } from "../components/entertainmentList/filters";
import { AllEntertainments } from "../components/entertainmentList/allEntertainments";

export const EntertainmentListPage = () => {
  return (
    <React.Fragment>
      <Filters />
      <AllEntertainments />
    </React.Fragment>
  );
};
