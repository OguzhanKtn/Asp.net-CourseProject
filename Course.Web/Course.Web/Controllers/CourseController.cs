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
    }
}
