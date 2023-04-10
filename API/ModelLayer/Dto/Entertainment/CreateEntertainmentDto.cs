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
    public class CreateEntertainmentDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public List<int> CitiesIds { get; set; }

        public List<int> CategoriesIds { get; set; }
    }
}
