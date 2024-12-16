namespace Udemy.Web.Models.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
