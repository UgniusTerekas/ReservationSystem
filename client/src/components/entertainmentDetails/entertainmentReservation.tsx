import { Button, DatePicker, Divider, Skeleton, message } from "antd";
import React, { useEffect, useState } from "react";
import dayjs, { Dayjs } from "dayjs";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import {
  createUserReservation,
  getEntertainmentReservations,
  getReservationFillData,
} from "../../services/reservationServices";
import {
  CreateUserReservationModel,
  EntertainmentReservationsModel,
} from "../../types/reservation";

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

  /* USE STATES */
  const [selectedDate, setSelectedDate] = useState<Dayjs | null>(null);
  const [selectedTime, setSelectedTime] = useState<Dayjs | null>(null);
  const [startTime, setStartTime] = useState<string>();
  const [endTime, setEndTime] = useState<string>();
  const [breakTime, setBreakTime] = useState<string>();
  const [periodTime, setPeriodTime] = useState<string>();
  const [createUserReservationModel, setCreateUserReservationModel] =
    useState<CreateUserReservationModel>();
  const [isLoading, setIsLoading] = useState(false);
  const [entertainmentReservations, setEntertainmentReservations] = useState<
    EntertainmentReservationsModel[]
  >([]);
  const [selectedTimes, setSelectedTimes] = useState<string[]>([]);

  /*------------------------------------------------------------------------ */

  /*  USE QUERY  */
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

  const entertainmentReservationQuery = useQuery({
    queryKey: selectedDate
      ? [`entertainmentReservations/${selectedDate.format("YYYY-MM-DD")}`]
      : [],
    queryFn: ({ signal }) => {
      return getEntertainmentReservations(
        signal,
        Number(id),
        selectedDate?.format("YYYY-MM-DD") ?? ""
      );
    },
    onSuccess: (data) => {
      setEntertainmentReservations(data);
    },
  });

  useEffect(() => {
    if (selectedDate) {
      entertainmentReservationQuery.refetch();
    }
  }, [selectedDate]);
  /*------------------------------------------------------------------------ */

  /*  HANDLERS  */
  const handleDateChange = async (date: Dayjs | null) => {
    setSelectedDate(date);
    setSelectedTime(null);
  };

  const handleTimeClick = (time: Dayjs) => {
    const timeStr = time.format("HH:mm");
    setSelectedTimes([timeStr]);
    setSelectedTime((prevState) =>
      prevState?.format("HH:mm") === timeStr ? null : time
    );
  };

  const entertainmentSelectedTimesMapped = entertainmentReservations.map(
    (element) => dayjs(element.time)
  );

  const disabledTime = (current: Dayjs) => {
    const repeatCount = query.data?.maxCount!;
    const countMap = entertainmentSelectedTimesMapped.reduce(
      (map, time) =>
        map.set(time.format("HH:mm"), (map.get(time.format("HH:mm")) || 0) + 1),
      new Map<string, number>()
    );
    const currentCount = countMap.get(current.format("HH:mm")) || 0;
    return currentCount >= repeatCount;
  };

  const disabledTimes: string[] = entertainmentSelectedTimesMapped
    .filter((time) => disabledTime(time))
    .map((time) => time.format("HH-mm"));

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

    if (breakMinutes !== 0 || periodMinutes !== 0) {
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
          const isDisabled = disabledTimes.some((disabledTime) =>
            disabledTime.match(time.format("HH-mm"))
          );
          if (!filledSlots.some((t) => t === time.format("HH:mm"))) {
            timeSlots.push(
              <div
                key={time.format("HH:mm")}
                style={{
                  ...availableTimeStyle,
                  ...(selectedTimes.includes(time.format("HH:mm")) && {
                    backgroundColor: "#6366F1",
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
    } else {
      for (
        startHour;
        startHour! <= endHour!;
        startHour! += periodHour! + breakHour!
      ) {
        const time = dayjs(selectedDate)
          .startOf("day")
          .add(startHour!, "hours")
          .add(startMinutes!, "minutes");
        const isDisabled = disabledTimes.some((disabledTime) =>
          disabledTime.match(time.format("HH-mm"))
        );
        if (!filledSlots.some((t) => t === time.format("HH:mm"))) {
          timeSlots.push(
            <div
              key={time.format("HH:mm")}
              style={{
                ...availableTimeStyle,
                ...(selectedTimes.includes(time.format("HH:mm")) && {
                  backgroundColor: "#6366F1",
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
      return timeSlots;
    }
  };

  const handleSubmit = async () => {
    setIsLoading(true);

    const periodTimeToBack = new Date(periodTime!);
    const timeString = periodTimeToBack
      .toLocaleTimeString([], {
        hour: "2-digit",
        minute: "2-digit",
      })
      .replace(/\s[AP]M$/, "");

    setCreateUserReservationModel({
      entertainmentId: Number(id),
      reservationDate: selectedDate?.format("YYYY-MM-DD") ?? "",
      reservationTime: selectedTime?.format("HH:mm") ?? "",
      reservationPeriod: timeString,
    });

    if (createUserReservationModel) {
      try {
        await createUserReservation(createUserReservationModel);
        success();
        setIsLoading(false);
        setSelectedTime(null);
        window.location.reload();
      } catch {
        error();
        setIsLoading(false);
      }
    }

    setIsLoading(false);
  };

  /*  MESSAGES  */
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
  /*------------------------------------------------------------------------ */

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
        <Skeleton active loading={entertainmentReservationQuery.isLoading}>
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
