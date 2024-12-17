using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;
using Udemy.Web.Models.Services.ViewModels.Auth;

namespace Udemy.Web.Controllers
{
    public class AuthController(UserService userService) : BaseController
    {
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid) 
            { 
                return View(model);
            }
            var result = await userService.SignInAsync(model);
            SuccessOrFail(result, "User login successfully!");

            if (result.IsFail)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid) 
            { 
                return View(model);
            }

            var result = await userService.SignUpAsync(model);

            SuccessOrFail(result,"User created successfully!");

            if (result.IsFail)
            {
                return View(model);
            }

            return RedirectToAction("SignIn", "Auth");
        }
    }
}
