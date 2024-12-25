using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Controllers
{
    [Authorize]
    public class BasketController(BasketService basketService) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await basketService.Get());
        }
        public async Task<IActionResult> AddBasketItem(Guid id)
        {
            await basketService.AddBasketItem(id);
            return RedirectToAction("Index","Basket");
        }
        public async Task<IActionResult> RemoveBasketItem(Guid id)
        {
            await basketService.RemoveBasketItem(id);
            return RedirectToAction("Index", "Basket");
        }

        public async Task<IActionResult> ApplyCoupon(string couponCode)
        {
            var result = await basketService.ApplyDiscount(couponCode);

            SuccessOrFail(result,result.Message);

            var basket = await basketService.Get();

            return RedirectToAction("Index",basket);
        }
    }
}
