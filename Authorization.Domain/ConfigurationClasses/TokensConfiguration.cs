namespace Authorization.Domain.ConfigurationClasses
{
    public class TokensConfiguration : ITokensConfiguration
    {
        public bool ShouldIssueRefreshTokens { get; set; }
        public int RefreshTokenExpirationTimeInHours { get; set; }
        public IJwtConfiguration JwtConfiguration { get; set; } = new JwtConfiguration();
    }
}
