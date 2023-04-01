import { Button, Layout } from "antd";
import Search from "antd/es/input/Search";
import React from "react";

const { Content } = Layout;

const imageUrl =
  "https://img.freepik.com/free-photo/landscape-morning-fog-mountains-with-hot-air-balloons-sunrise_335224-794.jpg?w=1380&t=st=1680193277~exp=1680193877~hmac=69d400048717afe310c2a4d8f44b4882d86695db75ecfa9322608975a77c696e";

const BackgroundStyles: React.CSSProperties = {
  backgroundImage: `url(${imageUrl})`,
  backgroundSize: "cover",
  backgroundPosition: "0px 11%",
  height: "52vh",
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
  color: "#fff",
  fontSize: "3rem",
  marginBottom: "30px",
  flexDirection: "column",
};

const searchContainer = {
  display: "flex",
  justifyContent: "center",
};

export const BackgroundImage = () => {
  return (
    <React.Fragment>
      <Layout>
        <Content>
          <div style={BackgroundStyles}>
            <h1>Sveiki atvykę!</h1>
            <div style={searchContainer}>
              <Search
                size="large"
                className="custom-input"
                placeholder="Įveskite norimą pramogos pavadinimą"
                enterButton={<Button type="primary">Ieškoti!</Button>}
              />
            </div>
          </div>
        </Content>
      </Layout>
    </React.Fragment>
  );
};
