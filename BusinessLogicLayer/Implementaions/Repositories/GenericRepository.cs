using BusinessLogicLayer.Interfaces.Repositories;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementaions.Repositories
{
    public class GenericRepository<T> : IGenericRespository<T> where T : class
    {
        private readonly ExpenseTrackingDbContext dbContext;

        public GenericRepository(ExpenseTrackingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(T Entity)
        => await dbContext.Set<T>().AddAsync(Entity);

        public void Delete(T Entity)
        => dbContext.Set<T>().Remove(Entity);

        public async Task<IEnumerable<T>> GetAllAsync()
        => await dbContext.Set<T>().ToListAsync();
        public void Update(T Entity)
        => dbContext.Set<T>().Update(Entity);
        
    }
}
