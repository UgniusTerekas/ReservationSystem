import { UploadOutlined } from "@ant-design/icons";
import { FileUpload, FileUploadHandlerEvent } from "primereact/fileupload";
import { Button, Divider, Space, Upload, message } from "antd";
import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { postImages } from "../../services/imageServices";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";

export const CreateEntertainmentImage = () => {
  const { entertainmentId } = useParams();
  const navigate = useNavigate();
  const [messageApi, contextHolder] = message.useMessage();

  const [fileList, setFileList] = useState<File[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const UploadThumbail = (event: FileUploadHandlerEvent) => {
    const preFiles: File[] = [];
    event.files.forEach((file) => {
      preFiles.push(file);
    });
    setFileList(preFiles);
  };

  const handleUpload = (info: any) => {
    let fileList = [...info.fileList];
    fileList = fileList.slice(-10);
    fileList = fileList.map((file) => {
      if (file.response) {
        file.url = file.response.url;
      }
      return file;
    });
    setFileList(fileList);
  };

  const handleRemove = (file: any) => {
    const index = fileList.indexOf(file);
    const newFileList = fileList.slice();
    newFileList.splice(index, 1);
    setFileList(newFileList);
  };

  const postRequestHandler = async () => {
    setIsLoading(true);

    const formData = new FormData();

    formData.append("id", entertainmentId!);

    fileList.forEach((image) => {
      const file = new File([image.slice()], image.name, { type: image.type });
      console.log(file);
      formData.append("fileNames", image.name);
      formData.append("images", image);
    });

    try {
      await postImages(formData);
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
      content: "Nepavyko įkelti nuotraukų!",
    });
  };

  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Galerijos Kūrimas
      </Divider>
      {contextHolder}
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
          <Button
            size="large"
            style={{ backgroundColor: "#6366F1", color: "white" }}
            loading={isLoading}
            onClick={postRequestHandler}
          >
            Issaugoti
          </Button>
        </Space>
      </div>
    </React.Fragment>
  );
};
