import { Button, Divider, Form, Input, Rate } from "antd";
import React, { useState } from "react";

const { TextArea } = Input;

export const EntertainmentReview = () => {
  const [form] = Form.useForm();
  const [rating, setRating] = useState<number | undefined>(undefined);

  const handleRatingChange = (value: number) => {
    setRating(value);
  };

  const handleSubmit = () => {
    form.validateFields().then((values) => {
      console.log("Form values:", values);
      console.log("Rating:", rating);
    });
  };

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Palikite atsiliepimą
      </Divider>
      <Form form={form} style={{ paddingInline: 30 }}>
        <Form.Item
          label={<p style={{ fontWeight: "bold" }}>Palikti Atsiliepimą</p>}
          name="reply"
          rules={[
            {
              required: true,
              message: "Užpildykite atsiliepimo tekstą",
            },
          ]}
        >
          <TextArea rows={4} />
        </Form.Item>
        <Form.Item
          label={<p style={{ fontWeight: "bold" }}>Įvertinkite pramogą</p>}
          name="rating"
          rules={[
            {
              required: true,
              message: "Parinkite įvertinimą",
            },
          ]}
        >
          <Rate allowHalf onChange={handleRatingChange} />
        </Form.Item>
        <Form.Item>
          <Button type="primary" onClick={handleSubmit}>
            Paskelbti
          </Button>
        </Form.Item>
      </Form>
    </React.Fragment>
  );
};
