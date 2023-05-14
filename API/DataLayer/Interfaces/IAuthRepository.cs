using DataLayer.Entities.User;
using ModelLayer.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserEntity> CheckUsername(string username);

        Task<UserPasswordsDto> GetUserPasswords(string username);

        Task<UserEntity> CreateUser(UserEntity user);
    }
}
