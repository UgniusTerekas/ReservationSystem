import { Card, Divider, Space, Spin } from "antd";
import React from "react";
import { useQuery } from "react-query";
import { Link } from "react-router-dom";
import { getCities } from "../../services/cityServices";

export const CitiesList = () => {
  const query = useQuery({
    queryKey: ["citiesList"],
    queryFn: ({ signal }) => getCities(signal),
  });

  if (query.isLoading) {
    return <Spin />;
  }

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 110 }}>
        <h1>Paieška pagal miestą</h1>
      </Divider>
      <Space
        size={16}
        wrap
        style={{ justifyContent: "center", display: "flex", padding: "0 30px" }}
      >
        {query.data?.map((city) => (
          <Link to={"/pramogos"} key={city.cityId}>
            <Card
              hoverable
              cover={
                <img
                  src={require(`../../assets/${city.cityImage.slice(
                    0,
                    -5
                  )}.jpeg`)}
                  alt={city.cityName}
                  className="cities-card-image"
                />
              }
              className="cities-card"
            >
              <h3 className="cities-card-title">{city.cityName}</h3>
            </Card>
          </Link>
        ))}
      </Space>
    </React.Fragment>
  );
};
