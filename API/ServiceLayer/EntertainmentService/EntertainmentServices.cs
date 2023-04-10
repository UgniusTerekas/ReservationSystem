using DataLayer.Entities.City;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Interfaces;
using DataLayer.Repositories.CityRepository.cs;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Gallery;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.EntertainmentService
{
    public class EntertainmentServices
    {
        private readonly IEntertainmentRepository _entertainmentRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICategoriesRepository _categoryRepository;
        private readonly IGalleryRepository _galleryRepository;
        private readonly IGalleryServices _galleryServices;

        public EntertainmentServices(
            IEntertainmentRepository entertainmentRepository,
            ICityRepository cityRepository,
            ICategoriesRepository categoriesRepository,
            IGalleryRepository galleryRepository,
            IGalleryServices galleryServices)
        {
            _entertainmentRepository = entertainmentRepository;
            _cityRepository = cityRepository;
            _categoryRepository = categoriesRepository;
            _galleryRepository = galleryRepository;
            _galleryServices = galleryServices;
        }

        public async Task<List<EntertainmentCardDto>> GetEntertainments()
        {
            var entertainments = await _entertainmentRepository.GetEntertainments();

            var entertainmentDto = entertainments
                 .Select(e => new EntertainmentCardDto
                 {
                     Id = e.EntertainmentId,
                     Name = e.EntertainmentName,
                     Price = e.Price,
                     Image = e.Gallery.Select(x => new GalleryDto
                     {
                         ImageId= x.ImageId,
                         ImageLocation= x.ImageLocation,
                         ImageName= x.ImageName,
                     }).FirstOrDefault(),
                     Rating = e.Reviews.Average(x => x.Rating),
                 })
                 .ToList();

            return entertainmentDto;
        }

        public async Task<List<EntertainmentCardDto>> GetEntertainments(int? cityId, int? categoryId)
        {
            var entertainments = await _entertainmentRepository.GetEntertainments(cityId, categoryId);

            var entertainmentDto = entertainments
                 .Select(e => new EntertainmentCardDto
                 {
                     Id = e.EntertainmentId,
                     Name = e.EntertainmentName,
                     Price = e.Price,
                     Image = e.Gallery.Select(x => new GalleryDto
                     {
                         ImageId = x.ImageId,
                         ImageLocation = x.ImageLocation,
                         ImageName = x.ImageName,
                     }).FirstOrDefault(),
                     Rating = e.Reviews.Average(x => x.Rating),
                 })
                 .ToList();

            return entertainmentDto;
        }

        public async Task<bool> CreateEntertainment(CreateEntertainmentDto createEntertainment)
        {
            var entertainmentEntity = new EntertainmentItemEntity
            {
                EntertainmentName = createEntertainment.Name,
                EntertainmentDescription = createEntertainment.Description,
                Price = createEntertainment.Price,
            };

            foreach (var item in createEntertainment.CitiesIds)
            {
                var city = await _cityRepository.GetOneCity(item);
                entertainmentEntity.Cities.Add(city);
            }

            foreach (var item in createEntertainment.CategoriesIds)
            {
                var category = await _categoryRepository.GetOneCategory(item);
                entertainmentEntity.Categories.Add(category);
            }

            var result = await _entertainmentRepository.CreateEntertainment(entertainmentEntity);

            return true;
        }
    }
}
