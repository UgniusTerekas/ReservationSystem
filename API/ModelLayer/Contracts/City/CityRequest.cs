using Microsoft.AspNetCore.Http;

namespace ModelLayer.Contracts.City
{
    public class CityRequest
    {
        public string CityName { get; set; }

        public IFormFile CityImage { get; set; }
    }
}
