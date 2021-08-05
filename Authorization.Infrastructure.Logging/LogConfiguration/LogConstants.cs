namespace Authorization.Infrastructure.Logging.LogConfiguration
{
    public static class LogConstants
    {
        public const string ServerIpAddress = nameof(ServerIpAddress);

        public const string ClientIpAddress = nameof(ClientIpAddress);

        public const string RequestMethod = nameof(RequestMethod);

        public const string RequestDisplayUrl = nameof(RequestDisplayUrl);

        public const string Host = nameof(Host);

        public const string OutputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message}{NewLine}{Exception}";
    }
}
