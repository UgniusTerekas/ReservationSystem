import { GetCategoriesList } from "./category";
import { GetCitiesList } from "./city";
import { GetGallery } from "./gallery";
import { GetReviews } from "./review";

export interface GetEntertainment {
  id: number;
  name: "string";
  price: number;
  image: GetGallery;
  rating: number;
}

export interface GetEntertainmentDetails {
  name: string;
  description: string;
  price: number;
  email: string;
  address: string;
  phoneNumber: string;
  cities: GetCitiesList[];
  categories: GetCategoriesList[];
  gallery: GetGallery[];
  reviews: GetReviews[];
}

export interface CreateEntertainment {
  name: string | undefined;
  description: string | undefined;
  price: number | undefined;
  email: string;
  address: string;
  phoneNumber: string;
  citiesIds: number[] | undefined;
  categoriesIds: number[] | undefined;
}
