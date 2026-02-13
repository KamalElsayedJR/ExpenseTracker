using BusinessLogicLayer.Interfaces.Repositories;
using DataAccessLayer.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementaions.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpenseTrackingDbContext dbContext;
        private readonly Hashtable _repo = new Hashtable();
        public UnitOfWork(ExpenseTrackingDbContext dbContext)
        {
            this.dbContext = dbContext;
            UserRepo = new UserRepository(dbContext);
            ExpenseRepo = new ExpenseRepository(dbContext);
        }
        public IUserRepository UserRepo { get; }
        public IExpenseRepository ExpenseRepo { get ; }

        public IGenericRespository<T> GenericRepo<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!_repo.ContainsKey(type))
            {
                var repo = new GenericRepository<T>(dbContext);
                _repo.Add(type, repo);
            }
            return (IGenericRespository<T>)_repo[type];
        }

        public async ValueTask DisposeAsync()
        => await dbContext.DisposeAsync();
        public async Task<int> SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
    }
}
