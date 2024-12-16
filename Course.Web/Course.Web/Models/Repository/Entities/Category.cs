namespace Udemy.Web.Models.Repository.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public List<Course> Courses { get; set; } = default!;
    }
}
