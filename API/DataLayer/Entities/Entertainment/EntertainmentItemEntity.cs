using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Entities.Gallery;
using DataLayer.Entities.Reservation;
using DataLayer.Entities.Review;
using DataLayer.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.EntertainmentItem
{
    public class EntertainmentItemEntity
    {
        public EntertainmentItemEntity() 
        {
            Cities = new HashSet<CityEntity>();
            Categories= new HashSet<CategoryEntity>();
            Reviews = new HashSet<ReviewEntity>();
            Gallery = new HashSet<GalleryEntity>();
            Reservations = new HashSet<ReservationEntity>();
        }


        [Key]
        public int EntertainmentId { get; set; }

        public string EntertainmentName { get; set; }
        
        public string EntertainmentDescription { get; set; }

        public double Price { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public ICollection<CityEntity> Cities { get; set; }

        public ICollection<CategoryEntity> Categories { get; set; }

        public ICollection<ReviewEntity> Reviews { get; set; }

        public ICollection<GalleryEntity> Gallery { get; set; }

        public ICollection<ReservationEntity> Reservations { get; set; }
    }
}
