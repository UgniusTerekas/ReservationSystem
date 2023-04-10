using DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Review
{
    public  class ReviewEntity
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public double Rating { get; set; }

        public string Review { get; set; }
    }
}
