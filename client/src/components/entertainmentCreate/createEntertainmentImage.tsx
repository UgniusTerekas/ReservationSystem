import { FileUpload, FileUploadHandlerEvent } from "primereact/fileupload";
import { Button, Divider, Form, Input, Space, TimePicker, message } from "antd";
import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { postImages } from "../../services/imageServices";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import { CreateReservationModel } from "../../types/reservation";
import { createEntertainmentReservation } from "../../services/reservationServices";

export const CreateEntertainmentImage = () => {
  const format = "HH:mm";
  const { entertainmentId } = useParams();
  const navigate = useNavigate();
  const [messageApi, contextHolder] = message.useMessage();

  const [fileList, setFileList] = useState<File[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [reservationModel, setReservationModel] =
    useState<CreateReservationModel>({
      entertainmentId: -1,
      startTime: "",
      endTime: "",
      breakTime: -1,
      maxCount: -1,
      period: -1,
    });

  const UploadThumbail = (event: FileUploadHandlerEvent) => {
    const preFiles: File[] = [];
    event.files.forEach((file) => {
      preFiles.push(file);
    });
    setFileList(preFiles);
  };

  const handleRemove = (file: any) => {
    const index = fileList.indexOf(file);
    const newFileList = fileList.slice();
    newFileList.splice(index, 1);
    setFileList(newFileList);
  };

  const handleChange = (name: string, value: string | number[] | undefined) => {
    setReservationModel((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = async () => {
    setIsLoading(true);
    handleChange("entertainmentId", entertainmentId);

    if (fileList.length !== 0) {
      const formData = new FormData();

      formData.append("id", entertainmentId!);

      fileList.forEach((image) => {
        formData.append("fileNames", image.name);
        formData.append("images", image);
      });
      await postImages(formData);
      setIsLoading(false);
    }

    try {
      await createEntertainmentReservation(reservationModel);
      setIsLoading(false);
      navigate("/pramogos");
    } catch {
      error();
      setIsLoading(false);
    }

    setIsLoading(false);
  };

  const error = () => {
    messageApi.open({
      type: "error",
      content: "Nepavyko įkelti duomenų!",
    });
  };

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Galerijos Kūrimas
      </Divider>
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Space direction="vertical">
          <FileUpload
            name="demo[]"
            multiple={true}
            auto
            maxFileSize={100000000}
            customUpload={true}
            uploadHandler={UploadThumbail}
            onRemove={handleRemove}
            emptyTemplate={
              <p className="m-0">Įkelkite nuotraukas vilkdami ant ekrano</p>
            }
          />
        </Space>
      </div>
      <Divider style={{ paddingInline: 30, borderColor: "black" }}>
        Rezervacijos Kurimas
      </Divider>
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
                  <p style={{ fontSize: 16 }}>Pradžios laikas:</p>
                  <TimePicker
                    key="startTime"
                    onChange={(time) =>
                      handleChange("startTime", time ? time.format(format) : "")
                    }
                    format={format}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Pabaigos laikas:</p>
                  <TimePicker
                    key="endTime"
                    onChange={(time) =>
                      handleChange("endTime", time ? time.format(format) : "")
                    }
                    format={format}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Pertrauka tarp rezervacijų:</p>
                  <TimePicker
                    key="breakTime"
                    onChange={(time) =>
                      handleChange("breakTime", time ? time.format(format) : "")
                    }
                    format={format}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>
                    Maksimalus rezervacijų skaičius:
                  </p>
                  <Input
                    onChange={(e) => handleChange("maxCount", e.target.value)}
                    style={{ width: 260 }}
                  />
                </Space>
                <Space wrap direction="horizontal">
                  <p style={{ fontSize: 16 }}>Rezervacijų Trukmė:</p>
                  <TimePicker
                    key="period"
                    onChange={(time) =>
                      handleChange("period", time ? time.format(format) : "")
                    }
                    format={format}
                    style={{ width: 260 }}
                  />
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
                  >
                    Išsaugoti
                  </Button>
                </Space>
              </Space>
            </Space>
          </div>
        </Form>
      </div>
    </React.Fragment>
  );
};
