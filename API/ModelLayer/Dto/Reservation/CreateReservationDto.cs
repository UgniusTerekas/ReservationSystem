using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Reservation
{
    public class CreateReservationDto
    {
        public int EntertainmentId { get; set; }

        public int UserId { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int BreakTime { get; set; }

        public int MaxCount { get; set; }
    }
}
