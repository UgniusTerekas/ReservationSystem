using DataLayer.Entities.Category;
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
    public class EntertainmentServices : IEntertainmentServices
    {
        private readonly IEntertainmentRepository _entertainmentRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICategoriesRepository _categoryRepository;

        public EntertainmentServices(
            IEntertainmentRepository entertainmentRepository,
            ICityRepository cityRepository,
            ICategoriesRepository categoriesRepository)
        {
            _entertainmentRepository = entertainmentRepository;
            _cityRepository = cityRepository;
            _categoryRepository = categoriesRepository;
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
                         ImageLocation = e.Gallery.First().ImageLocation,
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
                if (result == null)
                {
                    return false;
                }
            }

            return true;
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
