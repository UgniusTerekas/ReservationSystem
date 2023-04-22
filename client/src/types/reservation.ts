export interface CreateReservationModel {
  entertainmentId: number;
  startTime: string;
  endTime: string;
  breakTime: number;
  maxCount: number;
  period: number;
}

export interface ReservationFillDataModel {
  reservationId: number;
  entertainmentId: number;
  date: string;
  startTime: string;
  endTime: string;
  breakTime: string;
  periodTime: string;
  maxCount: number;
}

export interface CreateUserReservationModel {
  entertainmentId: number;
  reservationDate: string;
  reservationTime: string;
}

export interface EntertainmentReservationsModel {
  date: string;
  time: string;
}
