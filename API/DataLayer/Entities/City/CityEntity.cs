using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.City
{
    public class CityEntity
    {
        [Key]
        public int CityId { get; set; }

        public string CityName { get; set; }

        public string CityImage { get; set; }
    }
}
