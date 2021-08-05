namespace Authorization.Infrastructure.Logging.ConfigurationClasses
{
    public interface ILoggingConfiguration
    {
        public bool IsEnabledConsoleLog { get; }
        public bool IsEnabledFileLog { get; }
        public bool ShouldLogInJsonFormat { get; }

        public string GlobalLogMinimumLevel { get; }
        public string ConsoleLogMinimumLevel { get; }
        public string FileLogMinimumLevel { get; }

        public string LogFilePath { get; }
    }
}
