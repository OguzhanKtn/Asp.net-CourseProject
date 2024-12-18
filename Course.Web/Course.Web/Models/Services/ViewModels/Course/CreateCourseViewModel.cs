
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Udemy.Web.Models.Services.ViewModels.Course
{
    public record CreateCourseViewModel
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public IFormFile? PictureFile { get; set; }
        public string LearningGoal { get; set; } = default!;
        public decimal Price { get; set; }
        public int TotalHour { get; set; }
        public int CategoryId { get; set; }

        [ValidateNever]
        public SelectList CategoryList { get; set; }
    }
}
