namespace Udemy.Web.Models.Services.ViewModels.Course
{
    public class CourseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public string EducatorName { get; set; } = default!;
        public string? PictureUrl { get; set; }
        public string Price { get; set; } = default!;
        public int TotalHour { get; set; }
        public bool IsActive { get; set; }
        public string CreatedAt { get; set; } = default!;
        public string? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string CategoryName { get; set; } = default!;

        public string GetUpdatedAt()
        {
            return string.IsNullOrEmpty(UpdatedAt) ? "-" : UpdatedAt;
        }
    }
}
