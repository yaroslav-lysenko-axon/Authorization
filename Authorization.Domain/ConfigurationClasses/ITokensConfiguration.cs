namespace Authorization.Domain.ConfigurationClasses
{
    public interface ITokensConfiguration
    {
        public bool ShouldIssueRefreshTokens { get; }
        public int RefreshTokenExpirationTimeInHours { get; }
        public IJwtConfiguration JwtConfiguration { get; }
    }
}
