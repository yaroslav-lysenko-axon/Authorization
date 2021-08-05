using System.IdentityModel.Tokens.Jwt;
using Authorization.Domain.Models;

namespace Authorization.Domain.Services.Abstraction
{
    public interface IJwtService
    {
        JwtSecurityToken CreateJwt(IUser user);
        string GetJwtString(JwtSecurityToken token);
    }
}
