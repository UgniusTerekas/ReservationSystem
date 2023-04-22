using DataLayer.Entities.User;

namespace DataLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUser(int id);

        Task<bool> EditUser(UserEntity editUser);

        Task<bool> DeleteUser(UserEntity userEntity);
    }
}