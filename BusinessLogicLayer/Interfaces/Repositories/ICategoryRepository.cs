using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(string userId);
        Task<Category> GetCategoryByIdAsync(int categoryId, string userId);
        void DeleteCategoryAsync(int categoryId, string userId);
        Task<bool> ExistCategoryAsync(string name);
    }
}
