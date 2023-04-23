using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Reservation
{
    public class GetReservationInfoForCustomer
    {
        public int ReservationId { get; set; }

        public string EntertainmentName { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public double Price { get; set; }

        public string Duration { get; set; }

        public string Address { get; set; }
    }
}
