using BusinessLogicLayer.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Services
{
    public interface IAuthService
    {
        Task<BaseResponse> RegisterAsync(string UserName,string Email,string Password);
        Task<DataResponse<UserDto>> LoginAsync(string Email,string Password);
        string HashPassword(string Password);
        bool VerifyPassword(string HashedPassword,string TextPassword);

    }
}
