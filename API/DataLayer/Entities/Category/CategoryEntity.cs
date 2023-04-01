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
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public byte[] CategoryImage { get; set; }
    }
}
