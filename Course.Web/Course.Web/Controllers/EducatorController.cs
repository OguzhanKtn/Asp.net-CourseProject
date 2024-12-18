using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;
using Udemy.Web.Models.Services.ViewModels.Course;

namespace Udemy.Web.Controllers
{
    [Authorize(Roles = "educator")]
    public class EducatorController(CourseService courseService) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var result = await courseService.GetAllByUserIdAsync();
            return View(result.Data);
        }

        public async Task<IActionResult> CreateCourse()
        {
            return View(await courseService.LoadCreateCourseAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await courseService.LoadCreateCourseAsync(model);
                return View(model);
            }

            var result = await courseService.CreateCourseAsync(model);
            SuccessOrFail(result,"Course created successfully!");

            if (result.IsFail)
            {
                await courseService.LoadCreateCourseAsync(model);
                return View(model);
            }

            return RedirectToAction("Index");

        }
    }
}
