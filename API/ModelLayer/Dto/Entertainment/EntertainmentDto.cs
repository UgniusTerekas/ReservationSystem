using ModelLayer.Dto.Category;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Gallery;
using ModelLayer.Dto.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Entertainment
{
    public class EntertainmentDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public ICollection<GalleryDto> Gallery { get; set; }

        public ICollection<CityDto> Cities { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }
        
        public ICollection<ReviewDto> Reviews { get; set; }
    }
}
