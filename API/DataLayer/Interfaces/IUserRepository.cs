using DataLayer.Entities.User;

namespace DataLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUser(int id);
    }
}