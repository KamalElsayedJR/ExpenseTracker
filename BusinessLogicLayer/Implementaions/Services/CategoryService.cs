using AutoMapper;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uoW;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork UoW, IMapper mapper)
        {
            _uoW = UoW;
            _mapper = mapper;
        }
        public async Task<BaseResponse> CreateCategoryAsync(string Name, string UserId)
        {
            var category = new Category
            {
                Name = Name,
                UserId = UserId
            };
            if (await _uoW.CategoryRepo.ExistCategoryAsync(Name.Trim()))
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Category with the same name already exists."
                };
            }
            await _uoW.GenericRepo<Category>().AddAsync(category);
            var result = await _uoW.SaveChangesAsync();
            if (result > 0)
            {
                return new BaseResponse
                {
                    IsSuccess = true,
                    Message = "Category created successfully."
                };
            }
            else
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "Failed to create category."
                };
            }
        }
        public async Task<DataResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(string UserId)
        {
            var cats = _uoW.CategoryRepo.GetAllCategoriesAsync(UserId);
            if (cats is not null)
            {
                return new DataResponse<IEnumerable<CategoryDto>>
                {
                    IsSuccess = true,
                    Message = "Category retreived Successfully",
                    Data = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(cats)
                };
            }
            return new DataResponse<IEnumerable<CategoryDto>>
            {
                IsSuccess = false,
                Message = "No categories found for the user.",
            };
        }
        public async Task<DataResponse<CategoryDto>> GetCategoryByIdAsync(int id, string UserId)
        {
            var cat = await _uoW.CategoryRepo.GetCategoryByIdAsync(id, UserId);
            if (cat is null)
            {
                return new DataResponse<CategoryDto>()
                {
                    IsSuccess = false,
                    Message = "Category not found",
                };
            }
            return new DataResponse<CategoryDto>()
            {
                IsSuccess = true,
                Message = "Category retreived Successfully",
                Data = _mapper.Map<Category, CategoryDto>(cat)
            };
        }
        public async Task<BaseResponse> DeleteCategoryAsync(int id, string UserId)
        {
            var result = await _uoW.CategoryRepo.GetCategoryByIdAsync(id, UserId);
            if (result is null || result.Expenses.Count > 0)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Category is not found or 'Used' with Expenses"
                };
            }
            _uoW.GenericRepo<Category>().Delete(result);
            var res = await _uoW.SaveChangesAsync();
            if (res <=0)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Cant delete Category"
                };
            }
            return new BaseResponse()
            {
                IsSuccess = true,
                Message = "Category deleted successfully"
            };

        }
        public async Task<BaseResponse> UpdateCategoryAsync(int id, string Name, string UserId)
        {
            var cat = await _uoW.CategoryRepo.GetCategoryByIdAsync(id, UserId);
            if (cat is null)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Category Not Found"
                };
            }
            if (cat.Name != Name && !string.IsNullOrEmpty(Name.Trim()))
            {
                cat.Name = Name;
            }
            _uoW.GenericRepo<Category>().Update(cat);
            var result = await _uoW.SaveChangesAsync();
            if (result <= 0)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Cant update Category"
                };
            }
            return new BaseResponse()
            {
                IsSuccess = true,
                Message = "Category updated successfully"
            };
        }
    }
}
