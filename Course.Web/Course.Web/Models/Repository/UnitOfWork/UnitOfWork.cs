
namespace Udemy.Web.Models.Repository.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork,IDisposable
    {
        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose() 
        { 
            context.Dispose();
        }
    }
}
