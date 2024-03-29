﻿using System;
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

        public string BreakTime { get; set; }

        public int MaxCount { get; set; }

        public string Period { get; set; }
    }
}
