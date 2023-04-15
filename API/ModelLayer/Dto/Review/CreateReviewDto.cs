using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Review
{
    public class CreateReviewDto
    {
        public int EntertainmentId { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }
    }
}
