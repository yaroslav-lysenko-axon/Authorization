using System.Threading.Tasks;
using Authorization.Domain.Models;

namespace Authorization.Domain.Services.Abstraction
{
    public interface ITokenService
    {
        AccessToken IssueAccessToken(IUser user);
        Task<RefreshToken> IssueRefreshToken(User user, Client client);
    }
}
