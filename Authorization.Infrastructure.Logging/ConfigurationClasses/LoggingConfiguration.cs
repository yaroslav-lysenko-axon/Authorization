namespace Authorization.Infrastructure.Logging.ConfigurationClasses
{
    public class LoggingConfiguration : ILoggingConfiguration
    {
        public bool IsEnabledConsoleLog { get; set; }
        public bool IsEnabledFileLog { get; set; }
        public bool ShouldLogInJsonFormat { get; set; }

        public string GlobalLogMinimumLevel { get; set; }
        public string ConsoleLogMinimumLevel { get; set; }
        public string FileLogMinimumLevel { get; set; }

        public string LogFilePath { get; set; }
    }
}
