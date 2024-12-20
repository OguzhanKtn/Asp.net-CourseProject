namespace Udemy.Web.Models.Services.ViewModels.Basket
{
    public class BasketItemViewModel
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; } = default!;
        public string? PictureFile { get; set; }
        public decimal Price { get; set; }
    }
}
