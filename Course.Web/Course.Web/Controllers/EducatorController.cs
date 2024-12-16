using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Controllers
{
    public class EducatorController(CourseService courseService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = await courseService.GetAllByUserIdAsync();
            return View(result.Data);
        }

        public async Task<IActionResult> CreateCourse()
        {
            return View();
        }
    }
}
