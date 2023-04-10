using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Entities.Gallery;
using DataLayer.Entities.Review;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities.EntertainmentItem
{
    public class EntertainmentItemEntity
    {
        public EntertainmentItemEntity() 
        {
            Cities = new HashSet<City.CityEntity>();
            Categories= new HashSet<CategoryEntity>();
            Reviews = new HashSet<ReviewEntity>();
            Gallery = new HashSet<Gallery.GalleryEntity>();
        }


        [Key]
        public int EntertainmentId { get; set; }

        public string EntertainmentName { get; set; }
        
        public string EntertainmentDescription { get; set; }

        public double Price { get; set; }

        public ICollection<City.CityEntity> Cities { get; set; }

        public ICollection<CategoryEntity> Categories { get; set; }

        public ICollection<ReviewEntity> Reviews { get; set; }

        public ICollection<Gallery.GalleryEntity> Gallery { get; set; }
    }
}
