using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PresentationLayer.Models;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model.Email, model.Password);
            if (result.IsSuccess && result.Data is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Data.Id),
                    new Claim(ClaimTypes.Name, result.Data.UserName),
                    new Claim(ClaimTypes.Email, result.Data.Email)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                TempData["SuccessMessage"] = result.Message;
                return Redirect("../Home");
            }
            TempData["ErrorMessage"] = result.Message;
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.RegisterAsync(model.UserName, model.Email, model.Password);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Login");
            }
            TempData["Errors"] = result.Message;
            return View(model);
        }
        [Authorize]
        public IActionResult Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var model = new UserDto()
            {
                Id = userId,
                UserName = userName,
                Email = email
            };
            return View(model);
        }
        
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "Logout successful";
            return RedirectToAction("Login");
        }
    }
}
