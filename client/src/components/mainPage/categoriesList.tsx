/* eslint-disable react/jsx-no-undef */
import { Card, Divider, Space } from "antd";
import React from "react";
import { useQuery } from "react-query";
import { Link } from "react-router-dom";
import { getCategories } from "../../services/categoriesServices";

export const CategoriesList = () => {
  const query = useQuery({
    queryKey: ["categoriesList"],
    queryFn: ({ signal }) => getCategories(signal),
  });

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 110 }}>
        <h1>Paieška pagal kategoriją</h1>
      </Divider>
      <Space
        size={16}
        wrap
        style={{ justifyContent: "center", display: "flex", padding: "0 30px" }}
      >
        {query.data?.map((category) => (
          <Link to={"/"} key={category.categoryId}>
            <Card
              hoverable
              cover={
                <img
                  src={require(`../../assets/${category.categoryImage.slice(
                    0,
                    -5
                  )}.jpeg`)}
                  alt={category.categoryName}
                  className="cities-card-image"
                />
              }
              className="cities-card"
            >
              <h3 className="cities-card-title">{category.categoryName}</h3>
            </Card>
          </Link>
        ))}
      </Space>
    </React.Fragment>
  );
};
