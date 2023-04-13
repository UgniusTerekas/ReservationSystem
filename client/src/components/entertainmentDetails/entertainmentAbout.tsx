import { Card, Divider } from "antd";
import React from "react";

interface Props {
  name: string | undefined;
  description: string | undefined;
}

export const EntertainmentAbout = ({ name, description }: Props) => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Apie Pramogą
      </Divider>
      <div style={{ margin: "30px" }}>
        <Card title={name}>
          <p style={{ fontSize: 16 }}>{description}</p>
        </Card>
      </div>
    </React.Fragment>
  );
};
