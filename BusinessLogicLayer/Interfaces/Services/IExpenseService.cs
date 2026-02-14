using BusinessLogicLayer.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<BaseResponse> AddExpenseAsync(decimal Amount, string Title, DateOnly Date, string UserId,int catId);
        Task<DataResponse<IEnumerable<ExpenseDto>>> GetExpensesByUserIdAsync(string UserId);
        Task<DataResponse<ExpenseDto>> GetExpenseByIdAsync(int Id, string UserId);
        Task<BaseResponse> UpdateExpenseAsync(int Id, decimal? Amount, string? Title, DateOnly? Date, string UserId);
        Task<BaseResponse> DeleteExpenseAsync(int Id, string UserId);
    }
}
