using Udemy.Web.Models.Repository;
using Udemy.Web.Models.Repository.CourseRepository;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Repository.UnitOfWork;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Extensions
{
    public static class ServiceExt
    {
        public static void AddServices(this IServiceCollection Services)
        {
           Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
           Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           Services.AddScoped<IUnitOfWork,UnitOfWork>();
           Services.AddScoped<ICourseRepository,CourseRepository>();
           Services.AddScoped<CourseService>();
           Services.AddHttpContextAccessor();
        }
    }
}
