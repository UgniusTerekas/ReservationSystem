using DataLayer.Entities.Reservation;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Reservation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataBaseContext _dbContext;

        public ReservationRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateReservation(ReservationEntity reservationEntity)
        {
            _dbContext.Reservations.Add(reservationEntity);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ReservationEntity>> GetReservations()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<List<ReservationEntity>> GetReservationsForUser(int userId)
        {
            var reservationEntity = await _dbContext
                .Reservations
                .Include(e => e.Entertainment)
                .Where(x =>
                    x.UserId == userId
                    && x.Date != null
                    && x.ReservationTime != null
                    )
                .ToListAsync();

            return reservationEntity;
        }

        public async Task<List<ReservationEntity>> GetEntertainmentReservations(
            int entertainmentId,
            DateTime dateSelected)
        {
            return await _dbContext
                .Reservations
                .Where(x =>
                    x.EntertainmentId == entertainmentId
                    && x.Date == dateSelected
                    && x.ReservationTime != null)
                .ToListAsync();
        }

        public async Task<ReservationEntity> GetReservation(int reservationId)
        {
            return await _dbContext.Reservations.FindAsync(reservationId);
        }
    }
}
