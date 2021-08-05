namespace Authorization.Infrastructure.Persistence.ConfigurationClasses
{
    public interface IPersistenceConfiguration
    {
        public string Host { get; }
        public string Port { get; }
        public string User { get; }
        public string Password { get; }
        public string Database { get; }
        public string Schema { get; }

        public string GetConnectionString();
    }
}
