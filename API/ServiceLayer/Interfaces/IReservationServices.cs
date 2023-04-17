using ModelLayer.Dto.Reservation;

namespace ServiceLayer.Interfaces
{
    public interface IReservationServices
    {
        Task<bool> CreateReservation(CreateReservationDto createReservation, int id);

        Task<List<GetReservationsDto>> GetReservations();
    }
}