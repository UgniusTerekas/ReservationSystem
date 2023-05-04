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
        Task<int> CreateEntertainment(CreateEntertainmentDto createEntertainment, int id);

        Task<bool> DeleteEntertainment(int id);

        Task<List<EntertainmentCardDto>> GetEntertainments();

        Task<List<EntertainmentCardDto>> GetCityEntertainments(int cityId);

        Task<List<EntertainmentCardDto>> GetCategoryEntertainments(int categoryId);

        Task<bool> UpdateEntertainment(UpdateEntertainmentDto updateModel);

        Task<EntertainmentDto> GetEntertainmentDetails(int id);

        Task<List<GetEntertainmentForEditing>> GetEntertainmentForEditing(int adminId);
    }
}
