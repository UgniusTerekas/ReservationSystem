import { Button, DatePicker, Divider, Skeleton, message } from "antd";
import React, { useState } from "react";
import dayjs, { Dayjs } from "dayjs";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import {
  createUserReservation,
  getReservationFillData,
} from "../../services/reservationServices";
import { CreateUserReservationModel } from "../../types/reservation";

const availableTimeStyle: React.CSSProperties = {
  display: "inline-block",
  width: "100px",
  height: "50px",
  lineHeight: "50px",
  textAlign: "center",
  backgroundColor: "#fff",
  cursor: "pointer",
  margin: "5px",
};

const disabledTimeStyle: React.CSSProperties = {
  backgroundColor: "#ddd",
  cursor: "not-allowed",
  opacity: 0.5,
};

export const EntertainmentReservation = () => {
  const { id } = useParams();

  const [messageApi, contextHolder] = message.useMessage();

  const [selectedDate, setSelectedDate] = useState<Dayjs | null>(null);
  const [selectedTime, setSelectedTime] = useState<Dayjs | null>(null);
  const [startTime, setStartTime] = useState<string>();
  const [endTime, setEndTime] = useState<string>();
  const [breakTime, setBreakTime] = useState<string>();
  const [periodTime, setPeriodTime] = useState<string>();
  const [createUserReservationModel, setCreateUserReservationModel] =
    useState<CreateUserReservationModel>();
  const [isLoading, setIsLoading] = useState(false);

  const query = useQuery({
    queryKey: [`reservationFillData/${id || ""}`],
    queryFn: ({ signal }) => {
      return getReservationFillData(signal, Number(id));
    },
    onSuccess: (data) => {
      setStartTime(data.startTime);
      setEndTime(data.endTime);
      setBreakTime(data.breakTime);
      setPeriodTime(data.periodTime);
    },
  });

  const handleDateChange = async (date: Dayjs | null) => {
    setSelectedDate(date);
    setSelectedTime(null);
  };

  const [selectedTimes, setSelectedTimes] = useState<string[]>([]);

  const handleTimeClick = (time: Dayjs) => {
    const timeStr = time.format("HH:mm");
    setSelectedTimes([timeStr]);
    setSelectedTime((prevState) =>
      prevState?.format("HH:mm") === timeStr ? null : time
    );
  };

  const disabledTimes: Dayjs[] = [
    dayjs("2023-04-09 12:00"),
    dayjs("2023-04-09 13:00"),
    dayjs("2023-04-09 15:00"),
  ];

  const disabledTime = (current: Dayjs) => {
    return (
      disabledTimes.some(
        (time) => time.format("HH:mm") === current.format("HH:mm")
      ) ||
      current.isBefore(dayjs(selectedDate).startOf("day").add(9, "hours")) ||
      current.isAfter(dayjs(selectedDate).startOf("day").add(20, "hours"))
    );
  };

  const getTimeSlots = () => {
    const timeSlots = [];
    const filledSlots = [] as string[];

    const startDate = startTime ? new Date(startTime) : undefined;
    let startHour = startDate?.getHours();
    let startMinutes = startDate?.getMinutes();

    const endDate = endTime ? new Date(endTime) : undefined;
    let endHour = endDate?.getHours();

    const breakDate = breakTime ? new Date(breakTime) : undefined;
    let breakHour = breakDate?.getHours();
    let breakMinutes = breakDate?.getMinutes();

    const periodDate = periodTime ? new Date(periodTime) : undefined;
    let periodHour = periodDate?.getHours();
    let periodMinutes = periodDate?.getMinutes();

    if (periodHour === 12) {
      periodHour = 0;
    }

    for (
      startHour;
      startHour! <= endHour!;
      startHour! += periodHour! + breakHour!
    ) {
      if (startHour === endHour) {
        break;
      }
      startMinutes = 0;
      for (
        startMinutes;
        startMinutes! <= 60;
        startMinutes! += periodMinutes! + breakMinutes!
      ) {
        const time = dayjs(selectedDate)
          .startOf("day")
          .add(startHour!, "hours")
          .add(startMinutes!, "minutes");
        const isDisabled = disabledTime(time);
        if (!filledSlots.some((t) => t === time.format("HH:mm"))) {
          timeSlots.push(
            <div
              key={time.format("HH:mm")}
              style={{
                ...availableTimeStyle,
                ...(selectedTimes.includes(time.format("HH:mm")) && {
                  backgroundColor: "#7FBF7F",
                }),
                ...(selectedTime == null && { backgroundColor: "#fff" }),
                ...(isDisabled && disabledTimeStyle),
              }}
              onClick={!isDisabled ? () => handleTimeClick(time) : undefined}
            >
              {time.format("HH:mm")}
            </div>
          );
          filledSlots.push(time.format("HH:mm"));
        }
      }
    }
    return timeSlots;
  };

  const handleSubmit = async () => {
    setIsLoading(true);
    setCreateUserReservationModel({
      entertainmentId: Number(id),
      reservationDate: selectedDate?.format("YYYY-MM-DD") ?? "",
      reservationTime: selectedTime?.format("HH:mm") ?? "",
    });
    console.log(createUserReservationModel);
    if (createUserReservationModel) {
      try {
        await createUserReservation(createUserReservationModel);
        success();
        setIsLoading(false);
        setSelectedTime(null);
      } catch {
        error();
        setIsLoading(false);
      }
    }

    setIsLoading(false);
  };

  const success = () => {
    messageApi.open({
      type: "success",
      content: "Rezervacija sukurta!",
    });
  };

  const error = () => {
    messageApi.open({
      type: "error",
      content: "Rezervacija nesukurta! Įvyko klaida!",
    });
  };

  return (
    <React.Fragment>
      {contextHolder}
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Rezervacija
      </Divider>
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          paddingInline: 30,
        }}
      >
        <DatePicker value={selectedDate} onChange={handleDateChange} />
        <Skeleton active loading={query.isLoading}>
          {selectedDate && (
            <div style={{ marginTop: 20 }}>{getTimeSlots()}</div>
          )}
          <Button
            style={{ marginTop: 15 }}
            type="primary"
            onClick={handleSubmit}
            disabled={!selectedTime}
            loading={isLoading}
          >
            Submit
          </Button>
        </Skeleton>
      </div>
    </React.Fragment>
  );
};
