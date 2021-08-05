using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Domain.ConfigurationClasses;
using Authorization.Domain.Models;
using Authorization.Domain.Services.Abstraction;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Domain.Services
{
    public class JwtService : IJwtService
    {
        private static readonly Encoding SymmetricKeyEncoding = Encoding.UTF8;
        private readonly IJwtConfiguration _jwtConfiguration;
        private readonly ITimeProvider _timeProvider;

        public JwtService(IJwtConfiguration jwtConfiguration, ITimeProvider timeProvider)
        {
            _jwtConfiguration = jwtConfiguration;
            _timeProvider = timeProvider;
        }

        public JwtSecurityToken CreateJwt(IUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = CreateClaimsIdentity(user);
            var now = _timeProvider.UtcNow();
            var key = new SymmetricSecurityKey(SymmetricKeyEncoding.GetBytes(_jwtConfiguration.SymmetricKey));

            var token = tokenHandler.CreateJwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Authority,
                subject: claims,
                notBefore: now,
                expires: now.AddHours(_jwtConfiguration.ExpirationTimeInHours),
                issuedAt: now,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

            return token;
        }

        public string GetJwtString(JwtSecurityToken token) => new JwtSecurityTokenHandler().WriteToken(token);

        private static ClaimsIdentity CreateClaimsIdentity(IUser user)
        {
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));

            return claimsIdentity;
        }
    }
}
