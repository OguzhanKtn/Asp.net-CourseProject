namespace Udemy.Web.Models.Repository.Entities
{
    public interface IAuditSoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
