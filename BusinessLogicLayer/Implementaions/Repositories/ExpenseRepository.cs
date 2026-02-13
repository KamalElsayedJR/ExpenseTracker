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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseTrackingDbContext _dbContext;

        public ExpenseRepository(ExpenseTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Expense>> GetAllExpensesForSpcificUser(string userId)
        =>
            await _dbContext.Expenses.Where(e => e.UserId == userId).ToListAsync();
        public async Task<Expense> GetExpenseByIdForSpcificUser(int Id, string userId)
        => await _dbContext.Expenses.FirstOrDefaultAsync(e => e.UserId == userId && e.Id == Id);
        
    }
}
