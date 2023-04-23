using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Entertainment
{
    public class CreateUserReservationDto
    {
        public int EntertainmentId { get; set; }

        public string ReservationDate { get; set; }

        public string ReservationTime { get; set; }

        public string ReservationPeriod { get; set; }
    }
}
