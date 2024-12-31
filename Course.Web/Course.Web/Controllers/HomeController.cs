using System.Diagnostics;
using Udemy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;
using Udemy.Web.Models.Repository;
using Udemy.Web.Models.Repository.Entities;
using Newtonsoft.Json;

namespace Udemy.Web.Controllers
{
    public class HomeController(CourseService courseService, BasketService basketService, IGenericRepository<Category> repository) : Controller
    {

        public async Task<IActionResult> Index(int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy)
        {
            var result = await courseService.GetFilteredCoursesAsync(categoryId, minPrice, maxPrice, sortBy);

            var basketItemCount = await basketService.GetCount();

            var categories = await repository.GetAllAsync();
            TempData["categories"] = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
            });

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
