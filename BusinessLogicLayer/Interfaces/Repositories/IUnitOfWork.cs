using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository UserRepo { get; }
        IGenericRespository<T> GenericRepo<T>() where T : class;
        Task<int> SaveChangesAsync();
        public IExpenseRepository ExpenseRepo { get; }
        public ICategoryRepository CategoryRepo { get; }

    }
}
