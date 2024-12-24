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
                //Todo: error page will be configured
            }
            return View(result.Data);
        }

        public async Task<IActionResult> Search(string query)
        {
           var courses = await courseService.SearchCourseAsync(query);

            return View(courses.Data);
        }

        public async Task<IActionResult> GetFilteredCourses(string? searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy)
        {
            var courses = await courseService.GetFilteredCoursesAsync(searchTerm, categoryId, minPrice, maxPrice, sortBy);

            return View(courses.Data);
        }
    }
}
