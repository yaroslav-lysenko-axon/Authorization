using System.Threading.Tasks;
using Authorization.Domain.Models;

namespace Authorization.Domain.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> FindByName(string roleName);
    }
}
