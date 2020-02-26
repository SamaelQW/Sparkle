using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using Sparkle.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sparkle.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        #region Private Members
        private readonly UserService _service;

        #endregion

        #region Constructor
        public AccountController(UserService service)
        {
            _service = service;
        }
        #endregion


        #region Login Actions

        /// <summary>
        /// View for user logining
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }

        /// <summary>
        /// Post vervion after pressed login button
        /// </summary>
        /// <param name="details">Login and password user typed</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel details)
        {
            if (ModelState.IsValid)
            {
                User user = await _service.GetByUserNameAsync(details.UserName);
                if (user != null)
                {
                    await Authenticate(details.UserName);
                    return RedirectToAction("Profile", "Home");
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Invalid username or password");
            }
            return View(details);
        }

        /// <summary>
        /// Action which logging out user and redirect to <see cref="Login"/> action
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Debug.WriteLine($"Logout method: is auth {User.Identity.IsAuthenticated}");
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Register Actions
        [HttpGet]
        [AllowAnonymous]
       
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _service.GetByUserNameAsync(model.UserName);
                if (user == null)
                {
                    await _service.CreateAsync(new User()
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        Age = DateTime.Now.Year - Convert.ToInt32(model.Year),
                        Password = model.Password,
                        Name = model.Name,
                        Surname = model.Surname,
                        PostIds = null,
                        Status = EUserStatus.Active
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

        #endregion


        #region Helper Methods

        /// <summary>
        /// Sign in with user login
        /// </summary>
        /// <param name="userName">User login to create new cookie</param>
        /// <returns></returns>
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