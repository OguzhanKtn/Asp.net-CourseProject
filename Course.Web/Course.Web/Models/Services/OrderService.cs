using System.Security.Claims;
using Udemy.Web.Models.Repository;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Repository.UnitOfWork;
using Udemy.Web.Models.Services.ViewModels.Order;

namespace Udemy.Web.Models.Services
{
    public class OrderService(BasketService basketService,IGenericRepository<Order> repository,IHttpContextAccessor contextAccessor,IUnitOfWork unitOfWork)
    {
        public async Task<OrderViewModel> LoadOrder()
        {
            var orderViewModel = new OrderViewModel()
            {
                Basket = await basketService.Get()
            };

            return orderViewModel;
        }

        public async Task<ServiceResult> CreateOrder(OrderViewModel model)
        {
            var isPayment = true;

            if (isPayment == false) 
            {
                return ServiceResult.Error("Kredi kartından ödeme alınamadı");
            }

            var userId = contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var basket = await basketService.Get();

            var newOrder = new Order()
            {
                OrderCode = "ORD" + new Random().Next(111111, 999999),
                UserId = Guid.Parse(userId!),
                Items = []
            };

            foreach (var item in basket.Items!) 
            {
                newOrder.Items.Add(new OrderItem()
                {
                    CourseId = item.CourseId,
                    CourseTitle = item.Name,
                    CoursePrice = item.Price,
                });
            }
            newOrder.TotalPrice = newOrder.Items.Sum(x => x.CoursePrice);

            await repository.AddAsync(newOrder);
            await unitOfWork.CommitAsync();

            foreach (var item in newOrder.Items.Select(x => x.CourseId))
            {
                await basketService.RemoveBasketItem(item);
            }

            return ServiceResult.Success("Ödeme başarıyla gerçekleşmiştir.");
        }
    }
}
