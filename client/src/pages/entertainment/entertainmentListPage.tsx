import React, { useState } from "react";
import { Filters } from "../../components/entertainmentList/filters";
import { AllEntertainments } from "../../components/entertainmentList/allEntertainments";
import { useQuery } from "react-query";
import { getEntertainments } from "../../services/entertainment";
import { GetEntertainment } from "../../types/entertainment";
import { Skeleton } from "antd";

export const EntertainmentListPage = () => {
  const [entertainmentList, setEntertainmentList] = useState<
    GetEntertainment[]
  >([]);

  const { isLoading } = useQuery({
    queryKey: [`getEntertainments`],
    queryFn: ({ signal }) => getEntertainments(signal),
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
