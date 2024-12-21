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
            string cacheKey = GetBasketCacheKey();

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

            var hasBasketItem = hasBasketCache.Items.Any(x => x.CourseId == courseId);

            if (hasBasketItem) return;

            hasBasketCache.Items.Add(basketItemModel);

            await cacheService.Set(cacheKey, hasBasketCache);
        }

        private string GetBasketCacheKey()
        {
            var userId = contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cacheKey = $"Basket:{userId}";
            return cacheKey;
        }

        public async Task RemoveBasketItem(Guid courseId)
        {
            string cacheKey = GetBasketCacheKey();

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
            string cacheKey = GetBasketCacheKey();

            var hasBasketCache = await cacheService.Get<BasketViewModel>(cacheKey) ?? new BasketViewModel();
            return hasBasketCache;
        }

        public async Task<int> GetCount()
        {
            string cacheKey = GetBasketCacheKey();

            var hasBasketCache = await cacheService.Get<BasketViewModel>(cacheKey) ?? new BasketViewModel();

            return hasBasketCache.Items!.Count();
        }

        public async Task RemoveBasketCache()
        {
            string cacheKey = GetBasketCacheKey();
            await cacheService.Remove(cacheKey);
        }
    }
}
