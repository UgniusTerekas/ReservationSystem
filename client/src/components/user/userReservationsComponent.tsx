import { Divider } from "antd";
import React, { useState } from "react";
import { UserReservationsModel } from "../../types/reservation";
import { useQuery } from "react-query";
import { getUserReservations } from "../../services/reservationServices";
import { UserReservations } from "./userReservations";

export const UserReservationComponent = () => {
  const [userReservations, setUserReservations] = useState<
    UserReservationsModel[]
  >([]);

  useQuery({
    queryKey: ["userReservations"],
    queryFn: ({ signal }) => getUserReservations(signal),
    onSuccess: (data) => {
      setUserReservations(data);
    },
  });

  return (
    <React.Fragment>
      <Divider style={{ paddingInline: 30, borderColor: "black" }}>
        Vartotojo Rezervacijos
      </Divider>
      <div
        style={{
          width: "100%",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          paddingInline: 30,
        }}
      >
        <div
          style={{
            borderStyle: "solid",
            borderWidth: "1px",
            margin: "15px",
            padding: "15px",
            borderRadius: "5px",
            borderColor: "black",
          }}
        >
          <div style={{ display: "flex", flexWrap: "wrap" }}>
            {userReservations.map((reservation) => (
              <div
                key={reservation.reservationId}
                style={{ width: "50%", padding: "10px", marginBottom: 20 }}
              >
                <UserReservations reservation={reservation} />
              </div>
            ))}
          </div>
        </div>
      </div>
    </React.Fragment>
  );
};
