import React, { useState } from "react";
import { Filters } from "../../components/entertainmentList/filters";
import { AllEntertainments } from "../../components/entertainmentList/allEntertainments";
import { useQuery } from "react-query";
import {
  getCityEntertainments,
  getEntertainments,
} from "../../services/entertainment";
import { GetEntertainment } from "../../types/entertainment";
import { Skeleton } from "antd";
import { useParams } from "react-router-dom";

export const CityEntertainmentListPage = () => {
  const { cityId } = useParams();

  const [entertainmentList, setEntertainmentList] = useState<
    GetEntertainment[]
  >([]);

  const { isLoading } = useQuery({
    queryKey: [`getEntertainments`],
    queryFn: ({ signal }) => getCityEntertainments(signal, Number(cityId)),
    onSuccess: (data) => {
      setEntertainmentList(data);
    },
  });

  return (
    <React.Fragment>
      <Skeleton active loading={isLoading}>
        <Filters />
        <AllEntertainments entertainmentList={entertainmentList} />
      </Skeleton>
    </React.Fragment>
  );
};
