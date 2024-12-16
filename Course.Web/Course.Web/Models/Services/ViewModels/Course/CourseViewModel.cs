namespace Udemy.Web.Models.Services.ViewModels.Course
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public string EducatorName { get; set; } = default!;
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int TotalHour { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}
