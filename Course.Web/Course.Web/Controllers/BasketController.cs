using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Controllers
{
    public class BasketController(BasketService basketService) : Controller
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
    }
}
