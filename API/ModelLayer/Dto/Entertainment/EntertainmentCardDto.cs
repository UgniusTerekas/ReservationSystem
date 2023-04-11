using ModelLayer.Dto.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Entertainment
{
    public class EntertainmentCardDto
    {
        public int Id { get; set; }

        public GalleryDto? Image { get; set; }

        public string Name { get; set; }

        public double? Rating { get; set; }

        public double Price { get; set; }
    }
}
