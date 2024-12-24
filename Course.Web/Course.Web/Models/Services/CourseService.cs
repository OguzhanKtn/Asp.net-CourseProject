using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Udemy.Web.Models.Repository;
using Udemy.Web.Models.Repository.CourseRepository;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Repository.UnitOfWork;
using Udemy.Web.Models.Services.ViewModels.Course;
using static MassTransit.Logging.OperationName;

namespace Udemy.Web.Models.Services
{
    public class CourseService(IGenericRepository<Category> categoryRepository,ICourseRepository courseRepository,IHttpContextAccessor contextAccessor,IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
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
                IsActive = true,
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

            await courseRepository.AddAsync(newCourse);
            await unitOfWork.CommitAsync();

            return ServiceResult.Success("Kurs başarıyla oluşturulmuştur");
        }

        public async Task<CreateCourseViewModel> LoadCreateCourseAsync()
        {
            var createCourse = new CreateCourseViewModel();
            var categories = await categoryRepository.GetAllAsync();

            createCourse.CategoryList = new SelectList(categories,"Id","Name");

            return createCourse;
        }

        public async Task<CreateCourseViewModel> LoadCreateCourseAsync(CreateCourseViewModel model)
        {
            var categories = await categoryRepository.GetAllAsync();

            model.CategoryList = new SelectList(categories, "Id", "Name");

            return model;
        }

        public async Task<ServiceResult<IEnumerable<CourseViewModel>>> GetAllByUserIdAsync()
        {
            var userId = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                
            var courses = await courseRepository.GetCoursesByUserIdAsync(Guid.Parse(userId));

            if (!courses.Any())
            {
                return ServiceResult<IEnumerable<CourseViewModel>>.Success(new List<CourseViewModel>());
            }

            return ServiceHelper.CheckIfNullOrNot
            (
               courses,
               data => data.Select(course => new CourseViewModel
               {
                   Id = course.Id,
                   Title = course.Title,
                   IsActive = true,
                   ShortDescription = course.ShortDescription,
                   PictureUrl = course.PictureUrl,
                   Price = course.Price.ToString("C"),
                   TotalHour = course.TotalHour,
                   CreatedAt = course.CreatedAt.ToLongDateString(),
                   CategoryName = course.Category.Name
                   
               })
            );
        }

        public async Task<ServiceResult<CourseViewModel>> GetCourseByIdAsync( Guid id)
        {
            var course = await courseRepository.GetCourseByIdAsync( id );

            var user = await userManager.FindByIdAsync(course.CreatedBy.ToString());

            var courseModel = new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                IsActive = true,
                Description = course.Description,
                ShortDescription = course.ShortDescription,
                PictureUrl = course.PictureUrl,
                Price = course.Price.ToString("C"),
                TotalHour = course.TotalHour,
                CreatedAt = course.CreatedAt.ToLongDateString(),
                EducatorName = user!.GetFullName,
                CategoryName = course.Category.Name
            };

            return ServiceHelper.CheckIfNullOrNot(course,courseModel);
        }

        public async Task<ServiceResult<IEnumerable<CourseViewModel>>> GetAllAsync()
        {
            var courses = await courseRepository.GetCoursesAsync();

            if (!courses.Any())
            {
                return ServiceResult<IEnumerable<CourseViewModel>>.Success(new List<CourseViewModel>());
            }

            var userIds = courses.Select(c => c.CreatedBy.ToString()).Distinct().ToList();
            var users = await userManager.Users
                .Where(u => userIds.Contains(u.Id.ToString()))
                .ToListAsync();

            var userDictionary = users.ToDictionary(u => u.Id, u => u.GetFullName);

            return ServiceHelper.CheckIfNullOrNot(courses, data => data.Select(course => new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                PictureUrl = course.PictureUrl,
                Price = course.Price.ToString("C"),
                TotalHour = course.TotalHour,
                CreatedAt = course.CreatedAt.ToLongDateString(),
                EducatorName = userDictionary.ContainsKey(course.CreatedBy)
                    ? userDictionary[course.CreatedBy]
                    : "Unknown Educator",
                CategoryName = course.Category!.Name
            }));
        }
        public async Task<ServiceResult<List<CourseViewModel>>> SearchCourseAsync(string query)
        {
            var courses = await courseRepository.SearchCourseAsync(query);

            var userIds = courses.Select(c => c.CreatedBy.ToString()).Distinct().ToList();
            var users = await userManager.Users
                .Where(u => userIds.Contains(u.Id.ToString()))
                .ToListAsync();

            var userDictionary = users.ToDictionary(u => u.Id, u => u.GetFullName);

            return ServiceHelper.CheckIfNullOrNot(courses, data => data.Select(course => new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                PictureUrl = course.PictureUrl,
                Price = course.Price.ToString("C"),
                TotalHour = course.TotalHour,
                CreatedAt = course.CreatedAt.ToLongDateString(),
                EducatorName = userDictionary.ContainsKey(course.CreatedBy)
                    ? userDictionary[course.CreatedBy]
                    : "Unknown Educator",
                CategoryName = course.Category!.Name
            }).ToList());
        }

        public async Task<ServiceResult<List<CourseViewModel>>> GetFilteredCoursesAsync(string? searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice, string? sortBy)
        {
            var coursesQuery = courseRepository.GetFilteredCourses(searchTerm,categoryId,minPrice,maxPrice,sortBy);

            var userIds = coursesQuery.Select(c => c.CreatedBy.ToString()).Distinct().ToList();
            var users = await userManager.Users
                .Where(u => userIds.Contains(u.Id.ToString()))
                .ToListAsync();

            var userDictionary = users.ToDictionary(u => u.Id, u => u.GetFullName);

            return ServiceHelper.CheckIfNullOrNot(coursesQuery, data => data.Select(course => new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                PictureUrl = course.PictureUrl,
                Price = course.Price.ToString("C"),
                TotalHour = course.TotalHour,
                CreatedAt = course.CreatedAt.ToLongDateString(),
                EducatorName = userDictionary.ContainsKey(course.CreatedBy)
                   ? userDictionary[course.CreatedBy]
                   : "Unknown Educator",
                CategoryName = course.Category!.Name
            }).ToList());
        }
    }
}
