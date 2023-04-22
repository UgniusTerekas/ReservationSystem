using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Entities.Reservation;
using DataLayer.Interfaces;
using DataLayer.Repositories.CityRepository.cs;
using ModelLayer.Dto.Category;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Gallery;
using ModelLayer.Dto.Reservation;
using ModelLayer.Dto.Review;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ServiceLayer.EntertainmentService
{
    public class EntertainmentServices : IEntertainmentServices
    {
        private readonly IEntertainmentRepository _entertainmentRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICategoriesRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public EntertainmentServices(
            IEntertainmentRepository entertainmentRepository,
            ICityRepository cityRepository,
            ICategoriesRepository categoriesRepository,
            IUserRepository userRepository)
        {
            _entertainmentRepository = entertainmentRepository;
            _cityRepository = cityRepository;
            _categoryRepository = categoriesRepository;
            _userRepository = userRepository;
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
                     Image = e.Gallery.FirstOrDefault() != null ? new GalleryDto
                     {
                         ImageId = e.Gallery.First().ImageId,
                         ImageLocation = "https://localhost:7229" + e.Gallery.First().ImageLocation,
                         ImageName = e.Gallery.First().ImageName
                     } : null,
                     Rating = e.Reviews.Any() ? e.Reviews.Average(r => r.Rating) : 0.0
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
                     Image = e.Gallery.FirstOrDefault() != null ? new GalleryDto
                     {
                         ImageId = e.Gallery.First().ImageId,
                         ImageLocation = e.Gallery.First().ImageLocation,
                         ImageName = e.Gallery.First().ImageName
                     } : null,
                     Rating = e.Reviews.Any() ? e.Reviews.Average(r => r.Rating) : 0.0
                 })
                 .ToList();

            return entertainmentDto;
        }
        
        public async Task<EntertainmentDto> GetEntertainmentDetails(int id)
        {
            var existingEntertainment = await _entertainmentRepository.GetEntertainment(id);

            if (existingEntertainment == null)
            {
                return null;
            }

            var user = await _userRepository.GetUser(existingEntertainment.Reviews.Select(x => x.UserId).FirstOrDefault());

            var entertainmentDetails = new EntertainmentDto
            {
                Name = existingEntertainment.EntertainmentName,
                Price = existingEntertainment.Price,
                Description = existingEntertainment.EntertainmentDescription,
                Gallery = existingEntertainment.Gallery?.Select(c => new GalleryDto
                {
                    ImageId = c.ImageId,
                    ImageName = c.ImageName,
                    ImageLocation = c.ImageLocation
                })
                .ToList(),

                Cities = existingEntertainment.Cities.Select(c => new CityDto
                {
                    CityId = c.CityId,
                    CityName = c.CityName,
                    CityImage = c.CityImage
                })
                .ToList(),

                Reviews = existingEntertainment.Reviews?.Select(c => new ReviewDto
                {
                    Id = c.ReviewId,
                    Description = c.Review,
                    Username = user.UserName,
                    Rating = c.Rating,
                    EntertainmentId = c.EntertainmentId

                })
                .ToList(),

                Reservations = existingEntertainment.Reservations?.Select(c => new GetReservationsDto
                {
                    Id = c.ReservationId,
                    EntertainmentId = c.EntertainmentId,
                    UserId = c.UserId,
                    Date = c.Date,
                    StartTime = c.StartTime,
                    EndTime = c.EndTime,
                    BreakTime = c.BreakTime,
                    Period = c.PeriodTime,
                    MaxCount = c.MaxCount,
                    ReservationTime = c.ReservationTime
                })
                .ToList(),

            Categories = existingEntertainment.Categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CategoryImage = c.CategoryImage
            })
            .ToList(),
            };

            return entertainmentDetails;
        }

        public async Task<int> CreateEntertainment(
            CreateEntertainmentDto createEntertainment,
            int id)
        {
            var entertainmentEntity = new EntertainmentItemEntity
            {
                EntertainmentName = createEntertainment.Name,
                EntertainmentDescription = createEntertainment.Description,
                Price = createEntertainment.Price,
                UserId = id,
            };

            foreach (var item in createEntertainment.CitiesIds)
            {
                var city = await _cityRepository.GetOneCity(item);
                if (city != null)
                {
                    entertainmentEntity.Cities.Add(city);
                }
            }

            foreach (var item in createEntertainment.CategoriesIds)
            {
                var category = await _categoryRepository.GetOneCategory(item);
                if (category != null)
                {
                    entertainmentEntity.Categories.Add(category);
                }
            }         

            if (entertainmentEntity != null)
            {
                var result = await _entertainmentRepository.CreateEntertainment(entertainmentEntity);
                if (result != null)
                {
                    return result.EntertainmentId;
                }
            }

            return -1;
        }

        public async Task<bool> UpdateEntertainment(UpdateEntertainmentDto updateModel)
        {
            var existingEntertainment = await _entertainmentRepository.GetEntertainment(updateModel.Id);

            if (existingEntertainment == null)
            {
                return false;
            }

            if (updateModel.Name != null
                && updateModel.Price != null
                && updateModel.Description != null
                && updateModel.Cities != null
                && updateModel.Categories != null)
            {
                existingEntertainment.EntertainmentName = updateModel.Name;
                existingEntertainment.Price = updateModel.Price;
                existingEntertainment.EntertainmentDescription = updateModel.Description;
                existingEntertainment.Cities = (ICollection<CityEntity>)updateModel.Cities;
                existingEntertainment.Categories = (ICollection<CategoryEntity>)updateModel.Categories;
            }

            var result = await _entertainmentRepository.UpdateEntertainment(existingEntertainment);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteEntertainment(int id)
        {
            var existingEntertainment = await _entertainmentRepository.GetEntertainment(id);

            if (existingEntertainment == null)
            {
                return false;
            }

            try
            {
                await _entertainmentRepository.DeleteEntertainment(existingEntertainment);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
