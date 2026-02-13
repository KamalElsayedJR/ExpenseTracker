using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _expenseService.GetExpensesByUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(result.Data);
        }
        public IActionResult NewExpense()
        {
            var model = new ExpenseDto
            {
                Date = DateOnly.FromDateTime(DateTime.Today)
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewExpense(ExpenseDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _expenseService.AddExpenseAsync(model.Amount, model.Title, model.Date, User.FindFirstValue(ClaimTypes.NameIdentifier));
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
