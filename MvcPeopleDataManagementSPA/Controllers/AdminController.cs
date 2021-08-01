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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityAppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityAppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserList()
        {
            return View(_userManager.Users.ToList());

        }

        public async Task<IActionResult> Details(string id)
        {
            IdentityAppUser userFound = await _userManager.FindByIdAsync(id);

            if(userFound == null)
            {
                return RedirectToAction(nameof(UserList));
            }
            
            return View(userFound);
        }

        public async Task<IActionResult> RolesManagement(string id)
        {
            IdentityAppUser userFound = await _userManager.FindByIdAsync(id);

            if (userFound == null)
            {
                return RedirectToAction(nameof(UserList));
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(userFound);

            List<IdentityRole> identityRoles = _roleManager.Roles.ToList();

            RolesManagementViewModel rolesManagementViewModel = new RolesManagementViewModel(id, userRoles, identityRoles);

            return View(rolesManagementViewModel);
        }

        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            IdentityAppUser userFound = await _userManager.FindByIdAsync(userId);

            if (userFound == null)
            {
                return RedirectToAction(nameof(UserList));
            }

            var result = await _userManager.AddToRoleAsync(userFound, roleName);

            if (result.Succeeded)
            {
                return RedirectToAction("RolesManagement", new { id = userId });
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(userFound);

            List<IdentityRole> identityRoles = _roleManager.Roles.ToList();

            RolesManagementViewModel rolesManagementViewModel = new RolesManagementViewModel(userId, userRoles, identityRoles);

            ViewBag.ErrorMsg = "Failed to change role for user!";

            return View("RolesManagement", rolesManagementViewModel);
        }

        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            IdentityAppUser userFound = await _userManager.FindByIdAsync(userId);

            if (userFound == null)
            {
                return RedirectToAction(nameof(UserList));
            }

            var result = await _userManager.RemoveFromRoleAsync(userFound, roleName);

            if (result.Succeeded)
            {
                return RedirectToAction("RolesManagement", new { id = userId });
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(userFound);

            List<IdentityRole> identityRoles = _roleManager.Roles.ToList();

            RolesManagementViewModel rolesManagementViewModel = new RolesManagementViewModel(userId, userRoles, identityRoles);

            ViewBag.ErrorMsg = "Failed to change role for user!";

            return View("RolesManagement", rolesManagementViewModel);
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles.ToList());
        }

        [HttpGet]
        public IActionResult CreateRole() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ViewBag.ErrorMsg = "Role name needs to be 3 letters long and not consist of blank spaces";
                return View("CreateRole", roleName);
            }

            IdentityRole role = new IdentityRole(roleName);

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(RoleList));
            }

            ViewBag.ErrorMsg = "Role was not created.";
            return View("CreateRole", roleName);
        }
        
        public async Task<IActionResult> RemoveRole(string roleName)
        {           
            if (
                !string.IsNullOrWhiteSpace(roleName) 
                && 
                await _roleManager.RoleExistsAsync(roleName)
                )
            {
                IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                var result = await _roleManager.DeleteAsync(role);
            
                //ToDo - inform user of result
            }
            
            return RedirectToAction(nameof(RoleList)); 
        }
    }
}
