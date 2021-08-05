using System;
using Authorization.Infrastructure.Logging.ConfigurationClasses;
using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace Authorization.Infrastructure.Logging.LogConfiguration
{
    public static class LogProvider
    {
        public static Logger CreateLogger(ILoggingConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(ToLogLevel(configuration.GlobalLogMinimumLevel))
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            if (configuration.IsEnabledConsoleLog)
            {
                SelfLog.Enable(Console.Error);
                if (configuration.ShouldLogInJsonFormat)
                {
                    loggerConfiguration.WriteTo.Console(new JsonFormatter(), ToLogLevel(configuration.ConsoleLogMinimumLevel));
                }
                else
                {
                    loggerConfiguration.WriteTo.Console(ToLogLevel(configuration.ConsoleLogMinimumLevel), LogConstants.OutputTemplate);
                }
            }

            if (configuration.IsEnabledFileLog)
            {
                loggerConfiguration.WriteTo.File(configuration.LogFilePath, ToLogLevel(configuration.FileLogMinimumLevel), LogConstants.OutputTemplate);
            }

            return loggerConfiguration.CreateLogger();
        }

        private static LogEventLevel ToLogLevel(string logLevel)
        {
            if (string.IsNullOrEmpty(logLevel))
            {
                return LogEventLevel.Information;
            }

            return (LogEventLevel)Enum.Parse(typeof(LogEventLevel), logLevel);
        }
    }
}
