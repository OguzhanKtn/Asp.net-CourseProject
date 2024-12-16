namespace Udemy.Web.Models.Repository.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; } = default!;
    }
}
