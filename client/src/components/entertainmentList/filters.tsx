import { Divider, Input, Select, Slider, Space } from "antd";
import React from "react";
import { useState } from "react";

const { Option } = Select;

export const Filters = () => {
  const [price, setPrice] = useState<[number, number]>([0, 300]);
  const [category, setCategory] = useState<string>("");
  const [city, setCity] = useState<string>("");

  const handlePriceChange = (value: [number, number]) => {
    setPrice(value);
  };

  const handleCategoryChange = (value: string) => {
    setCategory(value);
  };

  const handleCityChange = (value: string) => {
    setCity(value);
  };

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Filtravimas
      </Divider>
      <Space direction="vertical" align="center">
        <Space direction="horizontal" align="center">
          <Space direction="vertical">
            <h1 style={{ display: "flex", justifyContent: "center" }}>Kaina</h1>
            <Slider
              range
              defaultValue={[0, 300]}
              min={0}
              max={300}
              onChange={handlePriceChange}
            />
            <Input
              prefix="Min"
              suffix="Eur"
              value={price[0]}
              onChange={(e) => setPrice([+e.target.value, price[1]])}
            />
            <Input
              prefix="Max"
              suffix="Eur"
              value={price[1]}
              onChange={(e) => setPrice([price[0], +e.target.value])}
            />
          </Space>
          <Space direction="vertical" align="center" style={{ marginLeft: 30 }}>
            <h1>Kategorijos</h1>
            <Select
              style={{ width: 200 }}
              value={category}
              onChange={handleCategoryChange}
            >
              <Option value="">All categories</Option>
              <Option value="electronics">Electronics</Option>
              <Option value="books">Books</Option>
              <Option value="clothing">Clothing</Option>
            </Select>
          </Space>
          <Space direction="vertical" align="center" style={{ marginLeft: 30 }}>
            <h1>Miestai</h1>
            <Select
              style={{ width: 200 }}
              value={city}
              onChange={handleCityChange}
            >
              <Option value="">All cities</Option>
              <Option value="electronics">Electronics</Option>
              <Option value="books">Books</Option>
              <Option value="clothing">Clothing</Option>
            </Select>
          </Space>
        </Space>
      </Space>
    </React.Fragment>
  );
};
