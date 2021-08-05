namespace Authorization.Domain.ConfigurationClasses
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public string Issuer { get; set; }
        public string Authority { get; set; }
        public int ExpirationTimeInHours { get; set; }
        public string SymmetricKey { get; set; }
    }
}
