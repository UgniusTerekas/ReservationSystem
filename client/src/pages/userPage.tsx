import React from "react";
import { UserDataComponent } from "../components/user/userDataComponent";
import { UserReservationComponent } from "../components/user/userReservationsComponent";

export const UserPage = () => {
  return (
    <React.Fragment>
      <UserDataComponent />
      <UserReservationComponent />
    </React.Fragment>
  );
};
