using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityAppUser> _userManager;
        private readonly SignInManager<IdentityAppUser> _signInManager;

        public AccountController(UserManager<IdentityAppUser> userManager, SignInManager<IdentityAppUser> signInManager) // Constructor Injection
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegViewModel userRegViewModel)
        {
             if (ModelState.IsValid)
             {
                IdentityAppUser user = new IdentityAppUser()
                {
                    UserName = userRegViewModel.UserName,
                    FirstName = userRegViewModel.FirstName,
                    LastName = userRegViewModel.LastName,
                    BirthDate = userRegViewModel.BirthDate,
                    Email = userRegViewModel.Email,
                    PhoneNumber = userRegViewModel.Phone
                };
                IdentityResult result = await _userManager.CreateAsync(user, userRegViewModel.Password);

                if (result.Succeeded)
                {
                    //TODO - sign in user
                    return RedirectToAction("Index", "People");
                }
                
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
             }

             return View();
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
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "People");
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Locked-out", "Too many login attempts");
                }
            }

            return View(loginViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
