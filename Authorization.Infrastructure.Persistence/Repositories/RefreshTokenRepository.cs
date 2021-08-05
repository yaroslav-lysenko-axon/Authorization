using System;
using System.Threading.Tasks;
using Authorization.Domain.Models;
using Authorization.Domain.Repositories;
using Authorization.Infrastructure.Persistence.Contexts;

namespace Authorization.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AuthContext context)
            : base(context.RefreshTokens)
        {
        }

        public async Task RevokeRefreshTokens(User user, Client client, RefreshTokenRevokeReason reason)
        {
            var now = DateTime.UtcNow;
            var existingRefreshTokens = await Find(x => x.User == user &&
                                                        x.Client == client &&
                                                        x.ExpireAt > now &&
                                                        x.RevokedAt == null);

            foreach (var existingRefreshToken in existingRefreshTokens)
            {
                existingRefreshToken.RevokedAt = now;
                existingRefreshToken.RevokeReason = reason.ToString();
            }
        }
    }
}
