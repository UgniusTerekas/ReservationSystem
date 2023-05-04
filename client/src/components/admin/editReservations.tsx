import React, { useState } from "react";
import { Button, Divider, Form, Input, Select, Space } from "antd";
import TextArea from "antd/es/input/TextArea";
import { CreateEntertainment } from "../../types/entertainment";
import { useQuery } from "react-query";
import { getCategories } from "../../services/categoriesServices";
import { getCities } from "../../services/cityServices";
import { GetCategoriesList } from "../../types/category";
import { GetCitiesList } from "../../types/city";

const { Option } = Select;

export const EditReservations = () => {
  const [createEntertainmentModel, setCreateEntertainment] =
    useState<CreateEntertainment>({
      name: "",
      price: undefined,
      phoneNumber: "",
      address: "",
      email: "",
      categoriesIds: undefined,
      citiesIds: undefined,
      description: "",
    });
  const [categories, setCategories] = useState<GetCategoriesList[]>([]);
  const [cities, setCities] = useState<GetCitiesList[]>([]);

  const categoriesQuery = useQuery({
    queryKey: ["categoriesDropdown"],
    queryFn: ({ signal }) => getCategories(signal),
    onSuccess: (data) => {
      setCategories(data);
    },
  });

  const citiesQuery = useQuery({
    queryKey: ["citiesDropdown"],
    queryFn: ({ signal }) => getCities(signal),
    onSuccess: (data) => {
      setCities(data);
    },
  });

  const handleChange = (name: string, value: string | number[]) => {
    setCreateEntertainment((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };
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
                    value="Oro balionas"
                    onChange={(e) => handleChange("name", e.target.value)}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Aprašymas:</p>
                  <TextArea
                    value="Skrydis oro balionu"
                    onChange={(e) =>
                      handleChange("description", e.target.value)
                    }
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Kaina:</p>
                  <Input
                    value="129"
                    onChange={(e) => handleChange("price", e?.target?.value)}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Telefono numeris:</p>
                  <Input
                    value="+37065846366"
                    onChange={(e) =>
                      handleChange("phoneNumber", e?.target?.value)
                    }
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>El.paštas:</p>
                  <Input
                    value="test@test.com"
                    onChange={(e) => handleChange("email", e?.target?.value)}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Adresas:</p>
                  <Input
                    value="Vilnius"
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
