using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Reservation;

namespace ServiceLayer.Interfaces
{
    public interface IReservationServices
    {
        Task<bool> CreateReservation(
            CreateReservationDto createReservation,
            int id);

        Task<List<GetReservationInfoForCustomer>> GetReservationsForUser(int userId);

        Task<List<GetReservationsDto>> GetReservations();

        Task<GetReservationFillDto> GetReservationFillData(int entertainmentId);

        Task<bool> CreateUserReservation(
            CreateUserReservationDto createUserReservation,
            int userId);

        Task<List<EntertainmentReservationDto>> GetEntertainmentReservations(
            int entertainmentId,
            string date);

        Task<bool> DeleteUserReservation(int reservationId);

        Task<List<GetAdminReservationsDto>> GetAdminReservations(int adminId);
    }
}