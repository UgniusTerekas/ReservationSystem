import {
  Button,
  Divider,
  Form,
  Input,
  Select,
  Skeleton,
  Space,
  message,
} from "antd";
import TextArea from "antd/es/input/TextArea";
import React, { useState } from "react";
import { GetCategoriesList } from "../../types/category";
import { GetCitiesList } from "../../types/city";
import { useQuery } from "react-query";
import { getCategories } from "../../services/categoriesServices";
import { getCities } from "../../services/cityServices";
import { CreateEntertainment } from "../../types/entertainment";
import { createEntertainment } from "../../services/entertainment";
import { useNavigate } from "react-router-dom";

const { Option } = Select;

export const EntertainmentCreate = () => {
  const navigate = useNavigate();

  const [messageApi, messageHandler] = message.useMessage();

  const [categories, setCategories] = useState<GetCategoriesList[]>([]);
  const [cities, setCities] = useState<GetCitiesList[]>([]);
  const [createEntertainmentModel, setCreateEntertainment] =
    useState<CreateEntertainment>({
      name: "",
      price: undefined,
      categoriesIds: undefined,
      citiesIds: undefined,
      description: "",
    });
  const [isLoading, setIsLoading] = useState(false);

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
    console.log(value);
  };

  const handleSubmit = async () => {
    try {
      setIsLoading(true);
      const response = await createEntertainment(createEntertainmentModel);
      setIsLoading(false);
      navigate(`/kurti/nuotrauka/${response.data}`);
    } catch {
      error();
      setIsLoading(false);
    }
  };

  const error = () => {
    messageApi.open({
      type: "error",
      content: "Nepavyko sukurti pramogos!",
    });
  };

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Pramogos Kūrimas
      </Divider>
      <Skeleton
        active
        loading={citiesQuery.isLoading || categoriesQuery.isLoading}
      >
        {messageHandler}
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <Form onFinish={handleSubmit}>
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
                      value={createEntertainmentModel.name}
                      onChange={(e) => handleChange("name", e.target.value)}
                      style={{ width: 260 }}
                    />
                  </Space>
                  <Space wrap direction="horizontal">
                    <p style={{ fontSize: 16 }}>Aprašymas:</p>
                    <TextArea
                      value={createEntertainmentModel.description}
                      onChange={(e) =>
                        handleChange("description", e.target.value)
                      }
                      style={{ width: 260 }}
                    />
                  </Space>
                  <Space wrap direction="horizontal">
                    <p style={{ fontSize: 16 }}>Kaina:</p>
                    <Input
                      value={createEntertainmentModel.price}
                      onChange={(e) => handleChange("price", e?.target?.value)}
                      style={{ width: 260 }}
                    />
                  </Space>
                  <Space wrap direction="horizontal">
                    <p style={{ fontSize: 16 }}>Kategorija:</p>
                    <Select
                      mode="multiple"
                      onClear={() => handleChange("categoriesIds", [])}
                      value={createEntertainmentModel.categoriesIds}
                      onChange={(values) =>
                        handleChange("categoriesIds", values)
                      }
                      allowClear
                      style={{ width: 260 }}
                    >
                      {categories.length > 0 &&
                        categories.map((category) => (
                          <Option
                            key={category.categoryName}
                            value={category.categoryId}
                          >
                            {category.categoryName}
                          </Option>
                        ))}
                    </Select>
                  </Space>
                  <Space wrap direction="horizontal">
                    <p style={{ fontSize: 16 }}>Miestas:</p>
                    <Select
                      mode="multiple"
                      onClear={() => handleChange("citiesIds", [])}
                      value={createEntertainmentModel.citiesIds}
                      onChange={(values) => handleChange("citiesIds", values)}
                      allowClear
                      style={{ width: 260 }}
                    >
                      {cities.length > 0 &&
                        cities.map((city) => (
                          <Option key={city.cityName} value={city.cityId}>
                            {city.cityName}
                          </Option>
                        ))}
                    </Select>
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
                    <Button
                      loading={isLoading}
                      htmlType="submit"
                      type="primary"
                      size="large"
                      disabled={
                        createEntertainmentModel.name?.length! < 1 ||
                        createEntertainmentModel.description?.length! < 1 ||
                        createEntertainmentModel.price! < 0 ||
                        createEntertainmentModel.price === undefined ||
                        createEntertainmentModel.categoriesIds === undefined ||
                        createEntertainmentModel.categoriesIds === undefined
                      }
                    >
                      Išsaugoti
                    </Button>
                  </Space>
                </Space>
              </Space>
            </div>
          </Form>
        </div>
      </Skeleton>
    </React.Fragment>
  );
};
