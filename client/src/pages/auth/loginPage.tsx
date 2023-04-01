import { LoginComponent } from "../../components/auth/loginComponent";

export const LoginPage = () => {
  return (
    <div
      style={{
        backgroundColor: "#d9d9d9",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        backgroundPosition: "center",
        backgroundRepeat: "no-repeat",
        backgroundSize: "cover",
        height: "100vh",
        width: "100vw",
        margin: -1,
      }}
    >
      <LoginComponent />
    </div>
  );
};
