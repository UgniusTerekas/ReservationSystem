import { LoginComponent } from "../../components/auth/loginComponent";

export const LoginPage = () => {
  return (
    <div
      style={{
        backgroundColor: "#bfbfbf",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        backgroundPosition: "center",
        backgroundRepeat: "no-repeat",
        backgroundSize: "cover",
        height: "100vh",
        width: "100vw",
      }}
    >
      <LoginComponent />
    </div>
  );
};
