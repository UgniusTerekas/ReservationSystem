import { EditReservations } from "../components/admin/editReservations";
import { ReservationCalculations } from "../components/admin/reservationCalculations";
import React from "react";

export const AdminDashboardPage = () => {
  return (
    <React.Fragment>
      <ReservationCalculations />
      <EditReservations />
    </React.Fragment>
  );
};
