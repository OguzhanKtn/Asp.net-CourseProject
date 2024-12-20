using System.Security.Claims;
using Udemy.Web.Caching;
using Udemy.Web.Models.Repository.CourseRepository;
using Udemy.Web.Models.Services.ViewModels.Basket;

namespace Udemy.Web.Models.Services
{
    public class BasketService(ICacheService cacheService,IHttpContextAccessor contextAccessor,ICourseRepository courseRepository)
    {
        public async Task AddBasketItem(Guid courseId)
        {
            var userId = contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cacheKey = $"Basket:{userId}";

            var hasBasketCache = await cacheService.Get<BasketViewModel>(cacheKey) ?? new BasketViewModel();

            hasBasketCache.Items ??= [];

            var basketItem = await courseRepository.GetCourseByIdAsync(courseId);

            var basketItemModel = new BasketItemViewModel
            {
                CourseId = courseId,
                Name = basketItem!.Title,
                PictureFile = basketItem.PictureUrl,
                Price = basketItem.Price
            };

            hasBasketCache.Items.Add(basketItemModel);

            await cacheService.Set(cacheKey, hasBasketCache);
        }   

        public async Task RemoveBasketItem(Guid courseId)
        {
            var userId = contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cacheKey = $"Basket:{userId}";

            var hasBasketCache = await cacheService.Get<BasketViewModel>(cacheKey);

            if (hasBasketCache is null) return;
            if (hasBasketCache.Items is null) return;

            var basketItem = hasBasketCache.Items.FirstOrDefault(x => x.CourseId == courseId);
            if (basketItem is null) return;

            hasBasketCache.Items.Remove(basketItem);

            await cacheService.Set(cacheKey, hasBasketCache);
        }

        public async Task<BasketViewModel> Get()
        {
            var userId = contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cacheKey = $"Basket:{userId}";

            var hasBasketCache = await cacheService.Get<BasketViewModel>(cacheKey) ?? new BasketViewModel();
            return hasBasketCache;
        }
    }
}
