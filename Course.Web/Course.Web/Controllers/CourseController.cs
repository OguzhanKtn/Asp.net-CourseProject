using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Controllers
{
    public class CourseController(CourseService courseService) : Controller
    {
        public async Task<IActionResult> Detail(Guid id)
        {
            var result = await courseService.GetCourseByIdAsync(id);
            if (result.IsFail)
            {
                return RedirectToAction("Error500","Error");
            }
            return View(result.Data);
        }

        public async Task<IActionResult> Search(string query)
        {
           var courses = await courseService.SearchCourseAsync(query);

            return View(courses.Data);
        }
    }
}
