﻿using DataLayer.Entities.Reservation;

namespace DataLayer.Interfaces
{
    public interface IReservationRepository
    {
        Task<bool> CreateReservation(ReservationEntity reservationEntity);

        Task<List<ReservationEntity>> GetReservations();

        Task<ReservationEntity> GetReservation(int reservationId);

        Task<List<ReservationEntity>> GetEntertainmentReservations(
            int entertainmentId,
            DateTime dateSelected);

        Task<List<ReservationEntity>> GetReservationsForUser(int userId);

        Task<bool> DeleteUserReservation(ReservationEntity reservation);

        Task<List<ReservationEntity>> GetAdminReservations(int adminId);
    }
}