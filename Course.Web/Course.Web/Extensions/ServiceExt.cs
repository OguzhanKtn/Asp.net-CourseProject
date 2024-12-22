using MassTransit;
using Udemy.Web.Caching;
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
           Services.AddScoped<ICacheService,DistributedCacheService>();
           Services.AddScoped<CourseService>();
           Services.AddScoped<BasketService>();
           Services.AddScoped<UserService>();
           Services.AddScoped<OrderService>();
            Services.AddHttpContextAccessor();
           Services.AddMemoryCache();
           Services.AddStackExchangeRedisCache(x =>
           {
               x.Configuration = "localhost:6379";
           });
           Services.AddDistributedMemoryCache();
        }

        public static void AddCookies(this IServiceCollection Services)
        {
            Services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder
                {
                    Name = "CourseAppCookie"
                };     

                opt.LoginPath = new PathString("/Auth/SignIn");
                opt.LogoutPath = new PathString("/Auth/SignOut");
                opt.AccessDeniedPath = new PathString("/Auth/AccessDenied");
                opt.Cookie = cookieBuilder;
                opt.ExpireTimeSpan = TimeSpan.FromDays(30);
                opt.SlidingExpiration = true;
            });
        }

        public static void AddRabbitMQ(this IServiceCollection Services,WebApplicationBuilder builder)
        {
            Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
                });
            });
        } 
    }
}
