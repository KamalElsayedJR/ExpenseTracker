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
        public IQueryable<Expense> QueryUserExpenses(string userId)
        =>_dbContext.Expenses.Include(e => e.Category).Where(e => e.UserId == userId);
        public async Task<Expense> GetExpenseByIdForSpcificUser(int Id, string userId)
        => await _dbContext.Expenses.Include(e => e.Category).FirstOrDefaultAsync(e => e.UserId == userId && e.Id == Id);
        
    }
}
