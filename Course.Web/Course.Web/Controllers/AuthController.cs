using Microsoft.AspNetCore.Mvc;

namespace Udemy.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        } 
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
