using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        public string Id { get; private set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
