using BusinessLogicLayer.Dtos;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class ExpenseCreateModel
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Category { get; set; } = new List<SelectListItem>();
    }
}
