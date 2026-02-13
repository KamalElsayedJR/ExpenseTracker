using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
