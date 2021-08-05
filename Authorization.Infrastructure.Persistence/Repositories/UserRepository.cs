using System.Threading.Tasks;
using Authorization.Domain.Models;
using Authorization.Domain.Repositories;
using Authorization.Infrastructure.Persistence.Contexts;

namespace Authorization.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuthContext context)
            : base(context.Users)
        {
        }

        public Task<User> FindByEmail(string email)
        {
            return FindFirst(x => x.Email == email);
        }
    }
}
