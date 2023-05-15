import React, { useEffect, useState } from "react";
import { Button, Divider, Form, Input, Skeleton, Space } from "antd";
import TextArea from "antd/es/input/TextArea";
import { GetEntertainmentDetails } from "../../types/entertainment";
import { useQuery } from "react-query";
import { getCategories } from "../../services/categoriesServices";
import { getCities } from "../../services/cityServices";
import { GetCategoriesList } from "../../types/category";
import { GetCitiesList } from "../../types/city";
import { AdminReservationsModel } from "../../types/reservation";
import { getAdminReservations } from "../../services/reservationServices";
import axios from "axios";

export const EditReservations = () => {
  const [reservations, setReservations] = useState<AdminReservationsModel[]>(
    []
  );
  const [isLoading, setIsLoading] = useState(true);
  const [entertainmentDetails, setEntertainmentDetails] =
    useState<GetEntertainmentDetails>();
  const [, setCategories] = useState<GetCategoriesList[]>([]);
  const [, setCities] = useState<GetCitiesList[]>([]);

  useQuery({
    queryKey: ["adminReservations"],
    queryFn: ({ signal }) => getAdminReservations(signal),
    onSuccess: (data) => {
      setReservations(data);
    },
  });

  const fetchEntertainmentDetails = (id: number | undefined) => {
    axios
      .get(
        `https:localhost:7229/api/Entertainment/entertainmentDetails?id=${
          reservations.at(0)?.entertainmentId
        }`
      )
      .then((response) => {
        const data = response.data;
        setEntertainmentDetails(data);
      });
  };

  useEffect(() => {
    if (reservations !== undefined) {
      if (reservations.at(0)?.entertainmentId) {
        fetchEntertainmentDetails(reservations.at(0)?.entertainmentId);
      }
    }
  }, [reservations]);

  useQuery({
    queryKey: ["categoriesDropdown"],
    queryFn: ({ signal }) => getCategories(signal),
    onSuccess: (data) => {
      setCategories(data);
    },
  });

  useQuery({
    queryKey: ["citiesDropdown"],
    queryFn: ({ signal }) => getCities(signal),
    onSuccess: (data) => {
      setCities(data);
    },
  });

  const handleChange = (name: string, value: string | number[]) => {};

  return (
    <React.Fragment>
      <Divider style={{ paddingInline: 30, borderColor: "black" }}>
        Pramogos Redagavimas
      </Divider>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Form>
          <div
            style={{
              marginTop: 20,
              width: 1000,
              boxShadow: "0 0 10px 0 rgba(0, 0, 0, 0.1)",
              padding: 10,
              display: "flex",
              justifyContent: "center",
              borderRadius: "10px",
              backgroundColor: "#f2f2f2",
              border: "1px solid #e8e8e8",
              textAlign: "end",
            }}
          >
            <Space size={"large"}>
              <Space direction="vertical">
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Pavadinimas:</p>
                  <Input
                    value={entertainmentDetails?.name}
                    onChange={(e) => handleChange("name", e.target.value)}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Aprašymas:</p>
                  <TextArea
                    value={entertainmentDetails?.description}
                    onChange={(e) =>
                      handleChange("description", e.target.value)
                    }
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Kaina:</p>
                  <Input
                    value={entertainmentDetails?.price}
                    onChange={(e) => handleChange("price", e?.target?.value)}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Telefono numeris:</p>
                  <Input
                    value={entertainmentDetails?.phoneNumber}
                    onChange={(e) =>
                      handleChange("phoneNumber", e?.target?.value)
                    }
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>El.paštas:</p>
                  <Input
                    value={entertainmentDetails?.email}
                    onChange={(e) => handleChange("email", e?.target?.value)}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Adresas:</p>
                  <Input
                    value={entertainmentDetails?.address}
                    onChange={(e) => handleChange("address", e?.target?.value)}
                    style={{ width: 260 }}
                  />
                </Space>

                <Space
                  wrap
                  direction="horizontal"
                  style={{
                    width: "100%",
                    display: "flex",
                    justifyContent: "center",
                  }}
                >
                  <Button htmlType="submit" type="primary" size="large">
                    Išsaugoti
                  </Button>
                </Space>
              </Space>
            </Space>
          </div>
        </Form>
      </div>
    </React.Fragment>
  );
};
