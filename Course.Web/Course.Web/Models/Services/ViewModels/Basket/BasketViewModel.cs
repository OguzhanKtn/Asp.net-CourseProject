namespace Udemy.Web.Models.Services.ViewModels.Basket
{
    public class BasketViewModel
    {
        public Guid Id { get; set; }
        public List<BasketItemViewModel>? Items { get; set; }

        public decimal TotalPrice => Items?.Sum(x => x.Price) ?? 0;
    }

}
