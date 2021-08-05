namespace Authorization.Domain.ConfigurationClasses
{
    public interface IJwtConfiguration
    {
        public string Issuer { get; }
        public string Authority { get; }
        public int ExpirationTimeInHours { get; }
        public string SymmetricKey { get; }
    }
}
