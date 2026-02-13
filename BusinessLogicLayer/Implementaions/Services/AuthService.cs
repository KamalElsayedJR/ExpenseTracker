using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementaions.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uoW;

        public AuthService(IUnitOfWork UoW)
        {
            _uoW = UoW;
        }
        public string HashPassword(string Password)
        => BCrypt.Net.BCrypt.HashPassword(Password);
        public async Task<DataResponse<UserDto>> LoginAsync(string Email, string Password)
        {
            var user = await _uoW.UserRepo.GetByEmailAsync(Email);
            if (user is null)
            {
                return new  DataResponse<UserDto>()
                {
                    IsSuccess = false,
                    Message = "Login failed",
                };
            }
            if (!VerifyPassword(Password, user.HashedPassword))
            {
                return new DataResponse<UserDto>()
                {
                    IsSuccess = false,
                    Message = "Login failed",
                };
            }

            return new DataResponse<UserDto>()
            {
                IsSuccess = true,
                Message = "Login successful",
                Data = new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                }
            };
        }
        public async Task<BaseResponse> RegisterAsync(string UserName, string Email, string Password)
        {
            if (await _uoW.UserRepo.GetByEmailAsync(Email) is not null)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Failed to create user",
                };
            }
            ;
            var hashedPassword = HashPassword(Password);
            var user = new User()
            {
                UserName = UserName,
                Email = Email,
                HashedPassword = hashedPassword
            };
            await _uoW.GenericRepo<User>().AddAsync(user);
            var Result = await _uoW.SaveChangesAsync();
            if (Result > 0) return new BaseResponse()
            {
                IsSuccess = true,
                Message = "User created successfully",
            };
            return new BaseResponse()
            {
                IsSuccess = false,
                Message = "Failed to create user",
            };
        }
        public bool VerifyPassword(string TextPassword,string HashedPassword )
        => BCrypt.Net.BCrypt.Verify(TextPassword,HashedPassword);


    }
}
