using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Implementaions.Services;
using BusinessLogicLayer.Interfaces.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllCategoriesAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return View();
            }
            return View(result.Data);
        }
        public async Task<IActionResult> NewCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewCategory(CategoryDto model)
        {
            var result = await _categoryService.CreateCategoryAsync(model.Name, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return View(model);
            }
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateCategoryAsync(model.Id, model.Name,User.FindFirstValue(ClaimTypes.NameIdentifier));
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
            var result = await _categoryService.DeleteCategoryAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
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
