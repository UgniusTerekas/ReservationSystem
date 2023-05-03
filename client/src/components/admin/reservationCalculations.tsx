import { Button, Divider, Skeleton, Table, message } from "antd";
import React, { useState } from "react";
import { useQuery } from "react-query";
import {
  deleteUserReservation,
  getAdminReservations,
} from "../../services/reservationServices";
import { AdminReservationsModel } from "../../types/reservation";

export const ReservationCalculations = () => {
  const [reservations, setReservations] = useState<AdminReservationsModel[]>(
    []
  );
  const [page, setPage] = useState(1);
  const [isDeleting, setIsDeleting] = useState(false);
  const [messageApi, contextHolder] = message.useMessage();

  const { isLoading } = useQuery({
    queryKey: ["adminReservations"],
    queryFn: ({ signal }) => getAdminReservations(signal),
    onSuccess: (data) => {
      setReservations(data);
    },
  });

  const totalPrice: number = reservations.reduce(
    (acc: number, reservation: AdminReservationsModel) => {
      return acc + reservation.price;
    },
    0
  );

  const handlePageChange = (page: number) => {
    setPage(page);
  };

  const handleDelete = async (id: number) => {
    setIsDeleting(true);
    try {
      await deleteUserReservation(id);
      setReservations(
        reservations.filter((reservation) => reservation.id !== id)
      );
      success();
      setIsDeleting(false);
    } catch {
      error();
      setIsDeleting(false);
    }
  };

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
    },
    {
      title: "Pavadinimas",
      dataIndex: "entertainmentName",
    },
    {
      title: "Slapyvardis",
      dataIndex: "username",
    },
    {
      title: "El.paštas",
      dataIndex: "email",
    },
    {
      title: "Data",
      dataIndex: "reservationTime",
      key: "reservationTime",
      render: (reservationTime: string) => {
        const date = new Date(reservationTime);
        return date.toLocaleString("lt-LT");
      },
    },
    {
      title: "Kaina",
      dataIndex: "price",
      render: (price: number) => `${price} €`,
    },
    {
      title: "",
      dataIndex: "delete",
      render: (_: any, record: AdminReservationsModel) => {
        return (
          <Button
            loading={isDeleting}
            type="primary"
            danger
            onClick={() => handleDelete(record.id)}
          >
            Ištrinti
          </Button>
        );
      },
    },
  ];

  const success = () => {
    messageApi.open({
      type: "success",
      content: "Operacija sėkminga!",
    });
  };

  const error = () => {
    messageApi.open({
      type: "error",
      content: "Operacija nesėkminga!",
    });
  };

  return (
    <React.Fragment>
      <Divider style={{ paddingInline: 30, borderColor: "black" }}>
        Rezervacijų Duomenys
      </Divider>
      <Skeleton loading={isLoading} active>
        {contextHolder}
        <div style={{ display: "flex", justifyContent: "space-between" }}>
          <div style={{ width: "30%" }}>
            <div
              style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                borderRadius: "50%",
                width: "100px",
                height: "100px",
                border: "4px solid black",
                margin: "0 auto",
              }}
            >
              <h2 style={{ textAlign: "center" }}>{reservations.length}</h2>
            </div>
            <h3 style={{ textAlign: "center" }}>Rezervacijų skaičius</h3>
          </div>
          <div style={{ width: "60%" }}>
            <Table
              columns={columns}
              dataSource={reservations}
              pagination={{
                current: page,
                pageSize: 10,
                total: reservations.length,
                onChange: handlePageChange,
              }}
            />
          </div>
          <div style={{ width: "30%" }}>
            <div
              style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                borderRadius: "50%",
                width: "100px",
                height: "100px",
                border: "4px solid black",
                margin: "0 auto",
              }}
            >
              <h2 style={{ textAlign: "center" }}>{totalPrice} €</h2>
            </div>
            <h3 style={{ textAlign: "center" }}>Gauta suma</h3>
          </div>
        </div>
      </Skeleton>
    </React.Fragment>
  );
};
