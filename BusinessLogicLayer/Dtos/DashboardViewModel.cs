using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos
{
    public class DashboardViewModel
    {
        public decimal Total { get; set; }
        public int Count { get; set; }
        public decimal ThisMonth { get; set; }
        public decimal LastMonth { get; set; }
        public decimal Last7Days { get; set; }
        public IEnumerable<CategoryExpenseDto> Categories { get; set; }
        public IEnumerable<ExpenseDto> RecentExpenses { get; set; }

    }
}
