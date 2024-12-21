using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;
using Udemy.Web.Models.Services.ViewModels.Order;

namespace Udemy.Web.Controllers
{
    [Authorize]
    public class OrderController(OrderService orderService) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await orderService.LoadOrder());
        }


        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
           var result = await orderService.CreateOrder(model);

            SuccessOrFail(result,result.Message);

            if (result.IsFail) 
            {
                return RedirectToAction("Index","Order");
            }
            return RedirectToAction("PaymentSuccess", "Order");
        }

        public async Task<IActionResult> PaymentSuccess()
        {
            return View();
        }
    }
}
