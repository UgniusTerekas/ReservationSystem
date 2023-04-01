import { Card, Input, Button, Alert, Form } from "antd";
import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useRecoilState } from "recoil";
import { LoginRequest } from "../../contracts/authRequest";
import { authTokenAtom, isValidToken } from "../../recoil/authStates";
import { postLoginRequest } from "../../services/authServices";
import { setLocalAccessoken } from "../../services/tokenServices";
import { LoginCredentials } from "../../types/userAuth";

const labelStyle: React.CSSProperties = {
  width: 80,
  fontWeight: "bold",
};

const inputStyle: React.CSSProperties = {
  maxWidth: 270,
};

export const LoginComponent = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showError, setShowError] = useState<boolean>(false);

  const [tokenValid, setAuthToken] = useRecoilState(authTokenAtom);
  const [, setTokenValidation] = useRecoilState(isValidToken);

  const navigate = useNavigate();

  const loginHandler = async () => {
    const crediantials: LoginCredentials = {
      username: username,
      password: password,
    };

    const request: LoginRequest = {
      userLoginRequest: crediantials,
    };

    const res = await postLoginRequest(request);

    if (res === "") {
      setShowError(true);
      return;
    }

    setLocalAccessoken(res);
    setAuthToken(true);
    setTokenValidation(true);
    navigate("/pagrindinis");
  };

  useEffect(() => {
    if (tokenValid) {
      navigate("/");
    }
  }, []);

  return (
    <Card
      title="Prisijungimas"
      style={{ width: "50vw", maxWidth: 500, textAlign: "center" }}
    >
      <Form name="login" onFinish={loginHandler} scrollToFirstError>
        <Form.Item
          name="username"
          label={<p style={labelStyle}>Slapyvardis</p>}
          rules={[
            {
              required: true,
              message: "Prašome įvesti slapyvardį!",
            },
          ]}
        >
          <Input
            onChange={(e) => setUsername(e.target.value)}
            style={inputStyle}
          />
        </Form.Item>
        <Form.Item
          name="password"
          label={<p style={labelStyle}>Slaptažodis</p>}
          rules={[
            {
              required: true,
              message: "Prašome įvesti slaptažodį!",
            },
          ]}
          hasFeedback
        >
          <Input.Password
            onChange={(e) => setPassword(e.target.value)}
            style={inputStyle}
          />
        </Form.Item>
        <Form.Item style={{ textAlign: "center" }}>
          <Button style={{ marginTop: 10 }} type="primary" htmlType="submit">
            Prisijungti
          </Button>
        </Form.Item>
        {showError && (
          <Alert
            style={{ maxWidth: "500px" }}
            message="Klaidingai įvesti duomenys!"
            type="error"
            showIcon
          />
        )}
      </Form>
    </Card>
  );
};
