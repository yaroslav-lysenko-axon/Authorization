using System.Threading.Tasks;
using Authorization.Domain.Models;
using Authorization.Domain.Repositories;
using Authorization.Infrastructure.Persistence.Contexts;

namespace Authorization.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(AuthContext context)
            : base(context.Clients)
        {
        }
    }
}
