using MassTransit;
using Microsoft.AspNetCore.Identity;
using Shared.Events;
using System.Security.Claims;
using Udemy.Web.Models.Repository;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Repository.UnitOfWork;
using Udemy.Web.Models.Services.ViewModels.Basket;
using Udemy.Web.Models.Services.ViewModels.Order;

namespace Udemy.Web.Models.Services
{
    public class OrderService(BasketService basketService,IGenericRepository<Order> repository,IHttpContextAccessor contextAccessor,IUnitOfWork unitOfWork,IPublishEndpoint publishEndpoint,UserManager<AppUser> userManager)
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

            await SendOrderCreatedEvent( userId, basket);

            foreach (var item in newOrder.Items.Select(x => x.CourseId))
            {
                await basketService.RemoveBasketItem(item);
            }

            return ServiceResult.Success("Ödeme başarıyla gerçekleşmiştir.");
        }

        private async Task SendOrderCreatedEvent(string? userId, BasketViewModel basket)
        {
            List<CourseEventData> courses = new List<CourseEventData>();

            foreach (var course in basket.Items!)
            {
                courses.Add(new CourseEventData(course.Name, course.Price.ToString("C"), $"https://localhost:7284/pictures/courses/{course.PictureFile}"));
            }

            var user = await userManager.FindByIdAsync(userId!);

            var orderCreatedEvent = new OrderCreatedEvent(courses, user!.UserName!, user.Email!);

            await publishEndpoint.Publish(orderCreatedEvent);
        }
    }
}
