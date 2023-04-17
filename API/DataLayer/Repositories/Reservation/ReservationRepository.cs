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
    }
}
