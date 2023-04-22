import { ConfigProvider } from "antd";
import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { RecoilRoot } from "recoil";
import { App } from "./App";
import "./App.css";
import { GoogleOAuthProvider } from "@react-oauth/google";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <ConfigProvider
      theme={{
        token: {
          colorPrimary: "#000000",
          colorText: "#000000",
          colorLink: "#000000",
        },
      }}
    >
      <BrowserRouter>
        <RecoilRoot>
          <GoogleOAuthProvider clientId="1073024156022-fs6c0dilpr3ge1fl29i4d5uih9rmlq8k.apps.googleusercontent.com">
            <App />
          </GoogleOAuthProvider>
        </RecoilRoot>
      </BrowserRouter>
    </ConfigProvider>
  </React.StrictMode>
);
