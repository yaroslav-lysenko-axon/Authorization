namespace Authorization.Infrastructure.Persistence.ConfigurationClasses
{
    public class PersistenceConfiguration : IPersistenceConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }

        public string GetConnectionString()
        {
            return $"User ID={User};" +
                   $"Password={Password};" +
                   $"Host={Host};" +
                   $"Port={Port};" +
                   $"Database={Database};" +
                   $"Search Path={Schema};";
        }
    }
}
