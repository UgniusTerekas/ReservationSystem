using DataLayer.Entities.Reservation;

namespace DataLayer.Interfaces
{
    public interface IReservationRepository
    {
        Task<bool> CreateReservation(ReservationEntity reservationEntity);

        Task<List<ReservationEntity>> GetReservations();
    }
}