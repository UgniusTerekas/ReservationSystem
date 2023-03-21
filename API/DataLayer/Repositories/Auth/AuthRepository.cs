using DataLayer.Entities.User;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataBaseContext _dbContext;

        public AuthRepository(DataBaseContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity> CheckUsername(string username)
        {
            var result = _dbContext.Users.FirstOrDefault(x => x.UserName == username);

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task<UserPasswordsDto> GetUserPasswords(string username)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (result == null)
            {
                return null;
            }

            return new UserPasswordsDto {
                PasswordHash = result.Password,
                PasswordSalt = result.PasswordSalt 
            };
        }

        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
