using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager,UserService userService) : Controller
    {
        public async Task<IActionResult> Users()
        {
            var result = await userService.GetUsersAsync();
            ViewBag.Roles = await roleManager.Roles.Select(r => r.Name).ToListAsync();
            return View(result.Data);
        }
        [HttpPost]
        public IActionResult AddRoleToUser(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                return Json(new { success = false, message = "User ID and Role are required." });
            }

            var user = userManager.FindByIdAsync(userId).Result;
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var result = userManager.AddToRoleAsync(user, role).Result;
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Role added successfully." });
            }

            return Json(new { success = false, message = "Failed to add role." });
        }


        public async Task<IActionResult> Dashboard()
        {
            return View();
        }
    }
}
