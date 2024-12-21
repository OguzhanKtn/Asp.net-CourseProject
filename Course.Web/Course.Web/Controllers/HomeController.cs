using System.Diagnostics;
using Udemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Controllers
{
    public class HomeController(CourseService courseService,BasketService basketService) : Controller
    {
  
        public async Task<IActionResult> Index()
        {
            var result = await courseService.GetAllAsync();

            var basketItemCount = await basketService.GetCount();

            TempData["basketCount"] = basketItemCount;

            return View(result.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
