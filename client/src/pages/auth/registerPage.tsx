import { RegisterComponent } from "../../components/auth/registerComponent";

export const RegisterPage = () => {
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
      <RegisterComponent />
    </div>
  );
};
