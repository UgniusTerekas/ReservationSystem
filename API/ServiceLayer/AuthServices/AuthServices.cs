using ModelLayer.Contracts.Auth;
using ServiceLayer.Interfaces;
using System.Security.Cryptography;
using DataLayer.Entities.User;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ServiceLayer.AuthServices
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthServices(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        private void CreatePasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
               new Claim("UserId", user.UserId.ToString()),
               new Claim("Username", user.UserName),
               new Claim("Email", user.UserEmail),
               new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                audience: _configuration.GetSection("Jwt:Audience").Value,
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {
            var result = await _authRepository.CheckUsername(registerRequest.UserRegisterDto.UserName);

            if (result != null)
            {
                return null;
            }

            CreatePasswordHash(registerRequest.UserRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var registeredUser = new UserEntity
            {
                RoleId = (int)ModelLayer.Enums.UserEnums.Roles.RegisteredUser,
                StateId = (int)ModelLayer.Enums.UserEnums.States.Active,
                UserName = registerRequest.UserRegisterDto.UserName,
                UserEmail = registerRequest.UserRegisterDto.Email,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                RegistrationDate= DateTime.UtcNow,
            };
            
            await _authRepository.CreateUser(registeredUser);

            return new RegisterResponse
            {
                IsSuccess = true,
            };
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _authRepository.CheckUsername(loginRequest.UserLoginRequest.Username);

            if (user == null)
            {
                return null;
            }

            var validation = VerifyPasswordHash(loginRequest.UserLoginRequest.Password, user.Password, user.PasswordSalt);

            if (!validation)
            {
                return null;
            }

            return new LoginResponse
            {
                TokenJWT = CreateToken(user)
            };
        }
    }
}