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
  reservationPeriod: string;
}

export interface EntertainmentReservationsModel {
  date: string;
  time: string;
}

export interface UserReservationsModel {
  reservationId: number;
  entertainmentName: string;
  date: string;
  time: string;
  price: number;
  duration: string;
  address: string;
}

export interface AdminReservationsModel {
  id: number;
  entertainmentName: string;
  entertainmentId: number;
  username: string;
  email: string;
  userId: number;
  reservationTime: string;
  price: number;
}

export interface AdminReservationsTableType {
  id: number;
  entertainmentName: string;
  username: string;
  email: string;
  reservationTime: string;
  price: number;
}
