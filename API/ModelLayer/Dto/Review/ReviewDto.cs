using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }
    }
}
