using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Contracts.City
{
    public class UpdateCityRequest
    {
        public int CityId { get; set; }

        public string CityName { get; set; }

        public IFormFile CityImage { get; set; }
    }
}
