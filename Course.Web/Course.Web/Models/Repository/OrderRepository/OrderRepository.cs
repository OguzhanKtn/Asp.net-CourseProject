using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Models.Repository.OrderRepository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<OrderWithUserDto>> OrdersWithUsers()
        {
            var result = from order in _context.Orders
                         join user in _context.Users
                         on order.UserId equals user.Id
                         select new OrderWithUserDto
                         {
                             UserInfo = user.GetFullName,
                             OrderCode = order.OrderCode,
                             Price = order.TotalPrice
                         };

            return Task.FromResult(result.ToList());
        }
    }

    public class OrderWithUserDto
    {
        public string UserInfo { get; set; }
        public string OrderCode { get; set; }
        public decimal Price { get; set; }
    }

}
