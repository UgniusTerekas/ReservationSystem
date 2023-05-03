using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Reservation
{
    public class GetAdminReservationsDto
    {
        public int Id { get; set; }

        public string EntertainmentName { get; set; }

        public int EntertainmentId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }


        public int UserId { get; set; }

        public string ReservationTime { get; set; }

        public double Price { get; set; }
    }
}
