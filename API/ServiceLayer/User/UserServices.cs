using DataLayer.Interfaces;
using ModelLayer.Contracts.Auth;
using ModelLayer.Dto.UserDto;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.User
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;

        public UserServices(
            IUserRepository userRepository, 
            IAuthRepository authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        public async Task<GetUserDataDto> GetUserData(int userId)
        {
            var userEntity = await _userRepository.GetUser(userId);

            return new GetUserDataDto
            {
                Username = userEntity.UserName,
                Email = userEntity.UserEmail,
                Role = userEntity.Role.Name,
                State = userEntity.State.Name,
                RegistrationDate = userEntity.RegistrationDate
            };
        }

        public async Task<bool> EditUserInfo(
            UpdateUserInfo updateUser,
            int id)
        {
            var userEntity = await _userRepository.GetUser(id);

            var existingUsername = await _authRepository.CheckUsername(updateUser.UserName);

            if (existingUsername != null)
            {
                return false;
            }

            if (updateUser.UserName != "" || updateUser.UserName != null)
            {
                userEntity.UserName = updateUser.UserName;
            }

            if (updateUser.Email != "" && updateUser.Email != null)
            {
                userEntity.UserEmail = updateUser.Email;
            }            

            return await _userRepository.EditUser(userEntity);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userEntity = await _userRepository.GetUser(id);

            return await _userRepository.DeleteUser(userEntity);
        }
    }
}
