import { Card, Divider } from "antd";
import React from "react";

export const EntertainmentAbout = () => {
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Apie Pramogą
      </Divider>
      <div style={{ margin: "30px" }}>
        <Card title="Entertainment">
          <p style={{ fontSize: 16 }}>
            Entertainment is a form of activity that holds the attention and
            interest of an audience or gives pleasure and delight. It can be an
            idea or a task, but is more likely to be one of the activities or
            events that have developed over thousands of years specifically for
            the purpose of keeping an audience's attention.
          </p>
          <p style={{ fontSize: 16 }}>
            Although people's attention is held by different things, because
            individuals have different preferences in entertainment, most forms
            are recognizable and familiar. Storytelling, music, drama, dance,
            and different kinds of performance exist in all cultures, were
            supported in royal courts, developed into sophisticated forms and
            over time became available to all citizens. The process has been
            accelerated in modern times by an entertainment industry that
            records and sells entertainment products.
          </p>
        </Card>
      </div>
    </React.Fragment>
  );
};
