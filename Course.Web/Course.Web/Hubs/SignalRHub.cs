using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Udemy.Web.Models.Repository;
using Udemy.Web.Models.Repository.CourseRepository;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Repository.OrderRepository;


namespace Udemy.Web.Hubs
{
    public class SignalRHub(UserManager<AppUser> userManager,IOrderRepository orderRepository,ICourseRepository courseRepository,AppDbContext context) : Hub
    {
        public async Task Statistics()
        {
            var educators = await userManager.GetUsersInRoleAsync("educator");
            var educatorCount = educators.Count();

            await Clients.All.SendAsync("EducatorCount", educatorCount);

            var orders = await orderRepository.GetAllAsync();
            var revenue = orders.Sum(x => x.TotalPrice).ToString("C");

            await Clients.All.SendAsync("Revenue", revenue);

            var courses = await courseRepository.GetAllAsync();
            var coursesCount = courses.Count();

            await Clients.All.SendAsync("CourseCount", coursesCount);

            var customers = await orderRepository.OrdersWithUsers();

            await Clients.All.SendAsync("CustomerInfo", customers);
        }
    }
}
