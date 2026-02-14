using BusinessLogicLayer.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse> CreateCategoryAsync(string name, string UserId);
        Task<DataResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(string UserId);
        Task<DataResponse<CategoryDto>> GetCategoryByIdAsync(int id, string UserId);
        Task<BaseResponse> DeleteCategoryAsync(int id, string UserId);
        Task<BaseResponse> UpdateCategoryAsync(int id, string Name,string UserId);
    }
}
