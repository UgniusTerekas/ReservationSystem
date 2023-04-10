using DataLayer.Entities.EntertainmentItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Gallery
{
    public class GalleryEntity
    {
        [Key]
        public int ImageId { get; set; }

        public string ImageName { get; set; }

        public string ImageLocation { get; set; }

        [ForeignKey("Entertainment")]
        public int EntertainmentId { get; set; }

        public virtual EntertainmentItemEntity Entertainment { get; set; }
    }
}
