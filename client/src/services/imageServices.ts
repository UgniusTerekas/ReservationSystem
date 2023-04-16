import axios from "axios";

const BACK_END_API = `https://localhost:7229`;

export const postImages = async (formData: FormData) => {
  const response = await axios.post(
    BACK_END_API + "/api/Gallery/uploadImage",
    formData,
    {
      headers: { "Content-Type": "multipart/form-data" },
    }
  );

  return response;
};
