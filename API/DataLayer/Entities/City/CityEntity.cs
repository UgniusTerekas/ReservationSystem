using DataLayer.Entities.EntertainmentItem;
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
        public CityEntity() 
        {
            Entertainments = new HashSet<EntertainmentItemEntity>();
        }

        [Key]
        public int CityId { get; set; }

        public string CityName { get; set; }

        public string CityImage { get; set; }

        public ICollection<EntertainmentItemEntity> Entertainments { get; set; }
    }
}
