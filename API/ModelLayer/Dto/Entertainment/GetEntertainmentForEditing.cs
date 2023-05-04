using ModelLayer.Dto.Category;
using ModelLayer.Dto.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.Entertainment
{
    public class GetEntertainmentForEditing
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public List<CityDto> Cities { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}
