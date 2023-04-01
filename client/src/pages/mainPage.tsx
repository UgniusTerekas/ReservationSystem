import React from "react";
import { BackgroundImage } from "../components/mainPage/backgroundImage";
import { CitiesList } from "../components/mainPage/citiesList";
import { CategoriesList } from "../components/mainPage/categoriesList";

export const MainPage = () => {
  return (
    <React.Fragment>
      <BackgroundImage />
      <CitiesList />
      <CategoriesList />
    </React.Fragment>
  );
};
