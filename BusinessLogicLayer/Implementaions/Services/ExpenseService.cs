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
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ExpenseService(IUnitOfWork Uow, IMapper mapper)
        {
            _uow = Uow;
            _mapper = mapper;
        }
        public async Task<BaseResponse> AddExpenseAsync(decimal Amount, string Title, DateOnly Date, string UserId,int catId)
        {
            var cat = await _uow.CategoryRepo.GetCategoryByIdAsync(catId,UserId);
            if (cat is null)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Category not found",
                };
            }
            if (Amount > 0.1m && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(UserId))
            {
                var expense = new Expense
                {
                    Amount = Amount,
                    Title = Title,
                    Date = Date,
                    UserId = UserId,
                    CategoryId = cat.Id,
                    Category = cat
                };
                await _uow.GenericRepo<Expense>().AddAsync(expense);
                var result = await _uow.SaveChangesAsync();
                if (result <= 0)
                {
                    return new BaseResponse
                    {
                        IsSuccess = false,
                        Message = "Failed to add expense"
                    };
                }
                return new BaseResponse
                {
                    IsSuccess = true,
                    Message = "Expense added successfully",
                };
            }
            return new BaseResponse
            {
                IsSuccess = false,
                Message = "Invalid inputs"
            };
        }
        public async Task<BaseResponse> DeleteExpenseAsync(int Id, string UserId)
        {
            var expense = await _uow.ExpenseRepo.GetExpenseByIdForSpcificUser(Id, UserId);
            if (expense is null)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Expense not found",
                };
            }
            _uow.GenericRepo<Expense>().Delete(expense);
            var deleteResult = await _uow.SaveChangesAsync();
            if (deleteResult <= 0)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Failed to delete expense",
                };
            }
            return new BaseResponse()
            {
                IsSuccess = true,
                Message = "Expense deleted successfully",
            };
        }
        public async Task<DataResponse<ExpenseDto>> GetExpenseByIdAsync(int Id, string UserId)
        {
            var expense = await _uow.ExpenseRepo.GetExpenseByIdForSpcificUser(Id, UserId);
            if (expense is null)
            {
                return new DataResponse<ExpenseDto>()
                {
                    IsSuccess = false,
                    Message = "Expense not found",
                };
            }
            return new DataResponse<ExpenseDto>()
            {
                IsSuccess = true,
                Message = "Expense retrieved successfully",
                Data = _mapper.Map<Expense, ExpenseDto>(expense),
            };
        }
        public async Task<DataResponse<IEnumerable<ExpenseDto>>> GetExpensesByUserIdAsync(string UserId)
        {
            var expenses = await _uow.ExpenseRepo.GetAllExpensesForSpcificUser(UserId);
            if (expenses is null)
            {
                return new DataResponse<IEnumerable<ExpenseDto>>
                {
                    IsSuccess = false,
                    Message = "No expenses found for the user",
                };
            }
            return new DataResponse<IEnumerable<ExpenseDto>>
            {
                IsSuccess = true,
                Message = "Expenses retrieved successfully",
                Data = _mapper.Map<IEnumerable<Expense>, IEnumerable<ExpenseDto>>(expenses),
            };
        }
        public async Task<BaseResponse> UpdateExpenseAsync(int Id, decimal? Amount, string? Title, DateOnly? Date, string UserId)
        {
            var expense = await _uow.ExpenseRepo.GetExpenseByIdForSpcificUser(Id, UserId);
            if (expense is null)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Expense not found",
                };
            }
            if (Amount is not null && Amount > 0.1m && expense.Amount != Amount)
            {
                expense.Amount = Amount.Value;
            }
            if (!string.IsNullOrEmpty(Title) && expense.Title != Title)
            {
                expense.Title = Title;
            }
            if (Date is not null && expense.Date != Date)
            {
                expense.Date = Date.Value;
            }
            _uow.GenericRepo<Expense>().Update(expense);
            var updateResult = await _uow.SaveChangesAsync();
            if (updateResult <= 0)
            {
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = "Failed to update expense",
                };
            }
            return new BaseResponse()
            {
                IsSuccess = true,
                Message = "Expense updated successfully",
            };

        }
    }
}
