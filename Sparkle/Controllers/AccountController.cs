using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using Sparkle.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sparkle.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserService _service;

        public AccountController(UserService service)
        {
            _service = service;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel details)
        {
            if (ModelState.IsValid)
            {
                User user = await _service.FindByNameAsync(details.UserName);

                if (user != null)
                {
                    await Authenticate(details.UserName);
                    return RedirectToAction("Profile", "Home");
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Invalid username or password");
            }
            return View(details);
        }



        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _service.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    await _service.CreateAsync(new User()
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        Age = DateTime.Now.Year - Convert.ToInt32(model.Year),
                        Password = model.Password,

                    });

                    await Authenticate(model.UserName);

                    return RedirectToAction("Profile", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login and(or) password");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        #region Helper Methods
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        #endregion

    }
}