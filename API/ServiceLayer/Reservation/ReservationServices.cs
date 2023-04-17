using DataLayer.Entities.Reservation;
using DataLayer.Interfaces;
using ModelLayer.Dto.Reservation;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Reservation
{
    public class ReservationServices : IReservationServices
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationServices(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

       public async Task<List<GetReservationsDto>> GetReservations()
        {
            var reservations = await _reservationRepository.GetReservations();

            return reservations
                .Select(x => new GetReservationsDto
                {
                    Id = x.ReservationId,
                    EntertainmentId= x.EntertainmentId,
                    UserId= x.UserId,
                    Date = x.Date,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    BreakTime = x.BreakTime,
                    MaxCount = x.MaxCount,
                })
                .ToList();
        }

        public async Task<bool> CreateReservation(
            CreateReservationDto createReservation,
            int id)
        {
            if (createReservation == null)
            {
                return false;
            }

            DateTime startTime = DateTime.ParseExact(createReservation.StartTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime endTime = DateTime.ParseExact(createReservation.EndTime, "HH:mm", CultureInfo.InvariantCulture);

            var reservationEntity = new ReservationEntity
            {
                EntertainmentId = createReservation.EntertainmentId,
                UserId = id,
                StartTime = startTime,
                EndTime = endTime,
                BreakTime = createReservation.BreakTime,
                MaxCount = createReservation.MaxCount,
            };

            return await _reservationRepository.CreateReservation(reservationEntity);
        }
    }
}
