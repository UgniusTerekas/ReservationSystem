import { Divider } from "antd";

type Props = {
  label: string;
};

export const SectionDivider = (props: Props) => {
  const { label } = props;

  return (
    <Divider orientation="left" style={{ borderColor: "black" }}>
      {label}
    </Divider>
  );
};
