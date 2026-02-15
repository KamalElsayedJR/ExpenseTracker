using BusinessLogicLayer.Interfaces.Repositories;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogicLayer.Implementaions.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ExpenseTrackingDbContext dbContext;
        public CategoryRepository(ExpenseTrackingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void DeleteCategoryAsync(int categoryId, string userId)
        => dbContext.Categories.Remove(dbContext.Categories.FirstOrDefault(c => c.Id == categoryId && c.UserId == userId));

        public async Task<bool> ExistCategoryAsync(string name)
        => await dbContext.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());

        public IQueryable<Category> GetAllCategoriesAsync(string userId)
        => dbContext.Categories.Where(c => c.UserId == userId);

        public async Task<Category> GetCategoryByIdAsync(int categoryId, string userId)
        => await dbContext.Categories.Include(c=>c.Expenses).FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
        
    }
}
