using DataLayer.Entities.EntertainmentItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Category
{
    public class CategoryEntity
    {
        public CategoryEntity() 
        {
            Entertainments = new HashSet<EntertainmentItemEntity>();
        }

        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryImage { get; set; }

        public ICollection<EntertainmentItemEntity> Entertainments { get; set; }
    }
}
