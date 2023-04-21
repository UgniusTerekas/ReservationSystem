using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Reservation
{
    public class GetReservationFillDto
    {
        public int ReservationId { get; set; }

        public int EntertainmentId { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string BreakTime { get; set; }

        public string PeriodTime { get; set; }

        public int MaxCount { get; set; }
    }
}
