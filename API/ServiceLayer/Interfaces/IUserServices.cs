using ModelLayer.Dto.UserDto;

namespace ServiceLayer.Interfaces
{
    public interface IUserServices
    {
        Task<GetUserDataDto> GetUserData(int userId);

        Task<bool> EditUserInfo(
            UpdateUserInfo updateUser,
            int id);

        Task<bool> DeleteUser(int id);
    }
}