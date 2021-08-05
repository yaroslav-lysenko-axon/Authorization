using System.Threading.Tasks;

namespace Authorization.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
        public Task Rollback();
    }
}
