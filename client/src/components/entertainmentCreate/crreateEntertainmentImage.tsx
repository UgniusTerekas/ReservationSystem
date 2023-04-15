import { Divider } from "antd";
import React from "react";
import { useParams } from "react-router-dom";

export const CreateEntertainmentImage = () => {
  const { entertainmentId } = useParams();
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Nuotraukos Kūrimas
      </Divider>
    </React.Fragment>
  );
};
