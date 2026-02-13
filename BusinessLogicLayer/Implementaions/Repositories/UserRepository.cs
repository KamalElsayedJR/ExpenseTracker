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
    public class UserRepository : IUserRepository
    {
        private readonly ExpenseTrackingDbContext dbContext;

        public UserRepository(ExpenseTrackingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User?> GetByEmailAsync(string Email)
        =>
            await dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
        
    }
}
