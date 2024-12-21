using Udemy.Web.Models.Services.ViewModels.Basket;

namespace Udemy.Web.Models.Services.ViewModels.Order
{
    public class OrderViewModel
    {
        public BasketViewModel Basket { get; set; } = default!;

        public string CardName { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
    }
}
