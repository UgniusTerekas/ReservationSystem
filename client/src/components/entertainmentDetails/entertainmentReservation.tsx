import { Button, DatePicker, Divider } from "antd";
import React, { useState } from "react";
import dayjs, { Dayjs } from "dayjs";

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
  const [selectedDate, setSelectedDate] = useState<Dayjs | null>(null);
  const [selectedTime, setSelectedTime] = useState<Dayjs | null>(null);

  const handleDateChange = (date: Dayjs | null) => {
    setSelectedDate(date);
    setSelectedTime(null);
  };

  const handleTimeClick = (time: Dayjs) => {
    setSelectedTime(time);
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
    const startHour = 9;
    const endHour = 20;

    for (let hour = startHour; hour <= endHour; hour++) {
      for (let minute = 0; minute <= 45; minute += 15) {
        const time = dayjs(selectedDate)
          .startOf("day")
          .add(hour, "hours")
          .add(minute, "minutes");
        const isDisabled = disabledTime(time);
        timeSlots.push(
          <div
            key={time.format("HH:mm")}
            style={{
              ...availableTimeStyle,
              ...(isDisabled && disabledTimeStyle),
            }}
            onClick={!isDisabled ? () => handleTimeClick(time) : undefined}
          >
            {time.format("HH:mm")}
          </div>
        );
      }
    }
    return timeSlots;
  };

  const handleSubmit = () => {
    console.log(`Selected date: ${selectedDate?.format("YYYY-MM-DD")}`);
    console.log(`Selected time: ${selectedTime?.format("HH:mm")}`);
  };

  return (
    <React.Fragment>
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
        {selectedDate && <div style={{ marginTop: 20 }}>{getTimeSlots()}</div>}
        <Button
          style={{ marginTop: 15 }}
          type="primary"
          onClick={handleSubmit}
          disabled={!selectedTime}
        >
          Submit
        </Button>
      </div>
    </React.Fragment>
  );
};
