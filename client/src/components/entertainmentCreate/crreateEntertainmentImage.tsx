import { UploadOutlined } from "@ant-design/icons";
import { Button, Divider, Space, Upload, message } from "antd";
import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { postImages } from "../../services/imageServices";

export const CreateEntertainmentImage = () => {
  const { entertainmentId } = useParams();
  const navigate = useNavigate();
  const [messageApi, contextHolder] = message.useMessage();

  const [fileList, setFileList] = useState<File[]>([]);
  const [isLoading, setIsLoading] = useState(false);

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
      var file = new File([image], image.name);
      formData.append("fileNames", image.name);
      formData.append("images", file);
    });

    try {
      await postImages(formData);
      setIsLoading(false);
      navigate("/");
    } catch {
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
          <Upload
            beforeUpload={() => false}
            multiple
            fileList={fileList as any[]}
            onChange={handleUpload}
            onRemove={handleRemove}
          >
            <Button icon={<UploadOutlined />}>Pasirinkti failus</Button>
          </Upload>
          <Button loading={isLoading} onClick={postRequestHandler}>
            Issaugoti
          </Button>
        </Space>
      </div>
    </React.Fragment>
  );
};
