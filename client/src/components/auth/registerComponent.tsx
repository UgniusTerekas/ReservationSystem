import { Button, Form, Input, Card, Alert } from "antd";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { RegisterCredentials } from "../../types/userAuth";
import { RegisterRequest } from "../../contracts/authRequest";
import { postRegisterRequest } from "../../services/authServices";
import { useRecoilState } from "recoil";
import { authTokenAtom, isValidToken } from "../../recoil/authStates";

const labelStyle: React.CSSProperties = {
  width: 80,
  fontWeight: "bold",
};

const inputStyle: React.CSSProperties = {
  maxWidth: 270,
};

export const RegisterComponent = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showError, setShowError] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState(false);
  const [isTokenValid, setTokenValidation] = useRecoilState(isValidToken);
  const [authToken] = useRecoilState(authTokenAtom);

  const navigate = useNavigate();

  const registerHandler = async () => {
    setIsLoading(true);
    const crediantials: RegisterCredentials = {
      username: username,
      email: email,
      password: password,
    };

    const request: RegisterRequest = {
      userRegisterDto: crediantials,
    };

    const res = await postRegisterRequest(request);

    if (res === false) {
      setIsLoading(false);
      setShowError(true);
      return;
    }
    setIsLoading(false);
    navigate("/prisijungimas");
  };

  useEffect(() => {
    if (isTokenValid) {
      navigate("/pagrindinis");
    }
  });

  return (
    <Card
      title="Registracija"
      style={{ width: "50vw", maxWidth: 500, textAlign: "center" }}
    >
      <Form name="register" onFinish={registerHandler} scrollToFirstError>
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
          name="email"
          label={<p style={labelStyle}>El. paštas</p>}
          rules={[
            {
              type: "email",
              message: "Prašome įvesti validų el. paštą!",
            },
            {
              required: true,
              message: "Prašome įvesti el. paštą!",
            },
          ]}
        >
          <Input
            onChange={(e) => setEmail(e.target.value)}
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
          <Input.Password style={inputStyle} />
        </Form.Item>

        <Form.Item
          name="confirm"
          label={<p style={labelStyle}>Pakartokite</p>}
          dependencies={["password"]}
          hasFeedback
          rules={[
            {
              required: true,
              message: "Pakartokite slaptažodį!",
            },
            ({ getFieldValue }) => ({
              validator(_, value) {
                if (!value || getFieldValue("password") === value) {
                  return Promise.resolve();
                }
                return Promise.reject(
                  new Error("Įvesti slaptažodžiai nesutampa!")
                );
              },
            }),
          ]}
        >
          <Input.Password
            onChange={(e) => setPassword(e.target.value)}
            style={inputStyle}
          />
        </Form.Item>

        <Form.Item style={{ textAlign: "center" }}>
          <Button
            loading={isLoading}
            style={{ marginTop: 10 }}
            type="primary"
            htmlType="submit"
          >
            Registruotis
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
