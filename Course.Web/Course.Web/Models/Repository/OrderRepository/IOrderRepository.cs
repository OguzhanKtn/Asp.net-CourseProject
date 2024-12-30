using Udemy.Web.Models.Repository.Entities;

namespace Udemy.Web.Models.Repository.OrderRepository
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<List<OrderWithUserDto>> OrdersWithUsers();
    }
}
