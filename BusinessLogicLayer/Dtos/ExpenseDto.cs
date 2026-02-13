using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateOnly Date { get; set; }
    }
}
