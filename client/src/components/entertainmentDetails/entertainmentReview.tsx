import { Button, Divider, Form, Input, Rate, message } from "antd";
import React, { useState } from "react";
import { createReview } from "../../services/reviewServices";
import { CreateReview } from "../../types/review";

const { TextArea } = Input;

export interface Props {
  id: number;
}

export const EntertainmentReview = ({ id }: Props) => {
  const [messageApi, messageHolder] = message.useMessage();

  const [form] = Form.useForm();

  const [rating, setRating] = useState<number>(-1);
  const [review, setReview] = useState<CreateReview>({
    entertainmentId: -1,
    rating: -1,
    description: "",
  });
  const [isLoading, setIsLoading] = useState(false);

  const handleRatingChange = (value: number) => {
    setRating(value);
  };

  const handleSubmit = async () => {
    setIsLoading(true);
    form.validateFields().then((values) => {
      setReview({
        ...review,
        entertainmentId: id,
        rating: rating,
        description: values.reply,
      });
    });

    try {
      await createReview(review);
      setIsLoading(false);
      window.location.reload();
    } catch {
      error();
      setIsLoading(false);
    }

    setIsLoading(false);
  };

  const error = () => {
    messageApi.open({
      type: "error",
      content: "Nepavyko išsaugoti atsiliepimo...",
    });
  };

  return (
    <React.Fragment>
      {messageHolder}
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
          <Button loading={isLoading} type="primary" onClick={handleSubmit}>
            Paskelbti
          </Button>
        </Form.Item>
      </Form>
    </React.Fragment>
  );
};
