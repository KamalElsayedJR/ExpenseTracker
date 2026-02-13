using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesForSpcificUser(string userId);
        Task<Expense> GetExpenseByIdForSpcificUser(int Id, string userId);
    }
}
