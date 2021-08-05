using System.Threading.Tasks;
using Authorization.Domain.Models;

namespace Authorization.Domain.Repositories
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task RevokeRefreshTokens(User user, Client client, RefreshTokenRevokeReason reason);
    }
}
