using AutoMapper;
using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService,ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;
            this._mapper = mapper;
            _expenseService = expenseService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _expenseService.GetExpensesByUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(result.Data);
        }
        public async Task<IActionResult> NewExpense()
        {
            var cats = await _categoryService.GetAllCategoriesAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var model = new ExpenseCreateModel
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Category = cats.Data.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewExpense(ExpenseCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _expenseService.AddExpenseAsync(model.Amount, model.Title, model.Date, User.FindFirstValue(ClaimTypes.NameIdentifier),model.CategoryId);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = result.Message;
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _expenseService.GetExpenseByIdAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _expenseService.UpdateExpenseAsync(model.Id, model.Amount, model.Title, model.Date, User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = result.Message;
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            var result = await _expenseService.DeleteExpenseAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
        }
    }
}
