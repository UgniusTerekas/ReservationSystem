export interface ReservationDate {
  dateTime: string;
  timeZone: string;
}

export interface GoogleEvent {
  summary: string;
  location: string;
  start: ReservationDate;
  end: ReservationDate;
  reminder: Reminder;
}

export interface Reminder {
  useDefault: boolean;
  overrides: Overide[];
}

export interface Overide {
  method: string;
  minutes: number;
}
