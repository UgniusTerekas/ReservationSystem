using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Reservation
{
    public class GetReservationsDto
    {
        public int Id { get; set; }

        public int EntertainmentId { get; set; }

        public int UserId { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? BreakTime { get; set; }

        public int? MaxCount { get; set; }

        public DateTime? Period { get; set; }

        public DateTime? ReservationTime { get; set;}
    }
}
