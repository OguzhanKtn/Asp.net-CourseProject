using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Udemy.Web.Models.Repository.CourseRepository;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Repository.UnitOfWork;
using Udemy.Web.Models.Services.ViewModels.Course;

namespace Udemy.Web.Models.Services
{
    public class CourseService(ICourseRepository repository,IHttpContextAccessor contextAccessor,IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
    {
        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
        {
            var userId = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var newCourse = new Course()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                Description = model.Description,
                LearningGoal = model.LearningGoal,
                Price = model.Price,
                TotalHour = model.TotalHour,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse(userId)
            };

            if (model.PictureFile is not null && model.PictureFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.PictureFile.FileName)}";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pictures","courses", fileName);

                await using var stream = new FileStream(path, FileMode.Create);

                await model.PictureFile.CopyToAsync(stream);

                newCourse.PictureUrl = fileName;
            }

            await repository.AddAsync(newCourse);
            await unitOfWork.CommitAsync();

            return ServiceResult.Success("Kurs başarıyla oluşturulmuştur");
        }

        public async Task<ServiceResult<IEnumerable<CourseViewModel>>> GetAllByUserIdAsync()
        {
            var userId = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                
            var courses = await repository.GetCoursesByUserIdAsync(Guid.Parse(userId));

            return ServiceHelper.CheckIfNullOrNot
            (
               courses,
               data => courses.Select(course => new CourseViewModel
               {
                   Title = course.Title,
                   ShortDescription = course.ShortDescription,
                   PictureUrl = course.PictureUrl,
                   Price = course.Price,
                   TotalHour = course.TotalHour,
                   CreatedAt = course.CreatedAt,
                   CategoryName = course.Category.Name
               })
            );
        }

        public async Task<ServiceResult<IEnumerable<CourseViewModel>>> GetAllAsync()
        {
            var courses = await repository.GetCoursesAsync();
               
            var userIds = courses.Select(c => c.CreatedBy.ToString()).Distinct().ToList();
            var users = await userManager.Users
                .Where(u => userIds.Contains(u.Id.ToString()))
                .ToListAsync();

            var userDictionary = users.ToDictionary(u => u.Id, u => u.GetFullName);

            return ServiceHelper.CheckIfNullOrNot(courses, data => data.Select(course => new CourseViewModel
            {
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                PictureUrl = course.PictureUrl,
                Price = course.Price,
                TotalHour = course.TotalHour,
                CreatedAt = course.CreatedAt,
                EducatorName = userDictionary.ContainsKey(course.CreatedBy)
                    ? userDictionary[course.CreatedBy]
                    : "Unknown Educator",
                CategoryName = course.Category!.Name
            }));
        }

    }
}
