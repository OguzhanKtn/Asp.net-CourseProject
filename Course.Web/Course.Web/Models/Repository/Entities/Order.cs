namespace Udemy.Web.Models.Repository.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string OrderCode { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;

        public Guid UserId { get; set; } = default!;
        public List<OrderItem> Items { get; set; } = default!;
    }

    public class OrderItem : BaseEntity<int> 
    {
        public Guid CourseId { get; set; } = default!;
        public string CourseTitle { get; set; } = default!;
        public decimal CoursePrice { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
    }
}
