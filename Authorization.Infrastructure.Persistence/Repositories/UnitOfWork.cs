using System.Threading.Tasks;
using Authorization.Domain.Repositories;
using Authorization.Infrastructure.Persistence.Contexts;

namespace Authorization.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthContext _databaseContext;

        public UnitOfWork(AuthContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task Commit()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Rollback()
        {
            await _databaseContext.DisposeAsync();
        }
    }
}
