using Microsoft.AspNetCore.Mvc;

namespace Udemy.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404(int? code)
        {
            ViewData["ErrorCode"] = code.HasValue ? code.Value : 404;  
            return View("Error404");
        }

        
        public IActionResult Error500()
        {
            ViewData["ErrorCode"] = 500; 
            return View("Error500");
        }
    }
}
