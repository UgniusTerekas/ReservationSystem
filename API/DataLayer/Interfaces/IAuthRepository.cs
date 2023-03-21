using DataLayer.Entities.User;
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

        Task<UserEntity> CreateUser(UserEntity user);
    }
}
