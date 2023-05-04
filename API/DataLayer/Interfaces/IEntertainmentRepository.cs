﻿using DataLayer.Entities.EntertainmentItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IEntertainmentRepository
    {
        Task<EntertainmentItemEntity> CreateEntertainment(EntertainmentItemEntity entertainment);
        Task DeleteEntertainment(EntertainmentItemEntity entertainment);
        Task<EntertainmentItemEntity> GetEntertainment(int entertainmentId);
        Task<List<EntertainmentItemEntity>> GetEntertainments();

        Task<List<EntertainmentItemEntity>> GetCityEntertainments(int cityId);

        Task<List<EntertainmentItemEntity>> GetCategoryEntertainments(int categoryId);
        Task<EntertainmentItemEntity> UpdateEntertainment(EntertainmentItemEntity entertainment);

        Task<List<EntertainmentItemEntity>> GetEntertainmentsForEdit(int adminId);
    }
}
