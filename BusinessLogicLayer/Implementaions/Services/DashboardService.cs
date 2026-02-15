using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementaions.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _uoW;
        public DashboardService(IUnitOfWork UoW)
        {
            _uoW = UoW;
        }
        public async Task<DashboardViewModel> GetDashboardData(string userId)
        {
            var Expenses = _uoW.ExpenseRepo.QueryUserExpenses(userId);
            var Total = await Expenses.SumAsync(e => (decimal ?) e.Amount) ?? 0m;
            var Count = await Expenses.CountAsync();
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var thisMonth = await Expenses.Where(e => e.Date.Year == today.Year && e.Date.Month == today.Month).SumAsync(e => (decimal?)e.Amount) ?? 0m;
            var lastMonthDate = today.AddMonths(-1);
            var lastMonth = await Expenses.Where(e => e.Date.Year == lastMonthDate.Year && e.Date.Month == lastMonthDate.Month).SumAsync(e => (decimal?)e.Amount) ?? 0m;
            var last7DaysDate = today.AddDays(-7);
            var last7Days = await Expenses.Where(e => e.Date >= last7DaysDate).SumAsync(e => (decimal?)e.Amount) ?? 0m;
            var recentExpenses = await Expenses.OrderByDescending(e => e.Date).Select(e => new ExpenseDto
            {
                Id = e.Id,
                Title = e.Title,
                Amount = e.Amount,
                Date = e.Date,
                CategoryId = e.CategoryId,
                CategoryName = e.Category.Name
            }).Take(7).ToListAsync();
            var categories = await Expenses.GroupBy(e => new { e.CategoryId, e.Category.Name }).Select(g => new CategoryExpenseDto
            {
                CategoryId = g.Key.CategoryId,
                CategoryName = g.Key.Name,
                TotalAmount = g.Sum(e => e.Amount)
            })
            .OrderByDescending(c => c.TotalAmount)
            .ToListAsync();
            return new DashboardViewModel
            {
                Total = Total,
                Count = Count,
                ThisMonth = thisMonth,
                LastMonth = lastMonth,
                Last7Days = last7Days,
                RecentExpenses = recentExpenses,
                Categories = categories
            };
        }
    }
}
