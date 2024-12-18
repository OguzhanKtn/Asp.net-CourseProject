using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Controllers
{
    public class AdminController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager) : Controller
    {
        public async Task<IActionResult> AddRoleToUser()
        {
            var newRole = new AppRole()
            {
                Name = "educator"
            };

            await roleManager.CreateAsync(newRole);

            var user = await userManager.FindByEmailAsync("o.kotan@mail.com");

            await userManager.AddToRoleAsync(user, "educator");

            return View();
        }
    }
}
