using ModelLayer.Dto.Entertainment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IEntertainmentServices
    {
        Task<bool> CreateEntertainment(CreateEntertainmentDto createEntertainment);
        Task<bool> DeleteEntertainment(int id);
        Task<List<EntertainmentCardDto>> GetEntertainments();
        Task<List<EntertainmentCardDto>> GetEntertainments(int? cityId, int? categoryId);
        Task<bool> UpdateEntertainment(UpdateEntertainmentDto updateModel);
    }
}
