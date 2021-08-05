using Authorization.Infrastructure.Logging.ConfigurationClasses;
using Authorization.Infrastructure.Logging.LogConfiguration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Authorization.Host
{
    public sealed class Program
    {
        private static void Main(string[] args)
        {
            var webHost = BuildWebHost(args);

            var loggingConfiguration = webHost.Services.GetRequiredService<ILoggingConfiguration>();
            Log.Logger = LogProvider.CreateLogger(loggingConfiguration);

            PrintStartupStatusMessages(webHost);

            webHost.Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((context, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                })
                .UseStartup<Startup>()
                .SuppressStatusMessages(true)
                .Build();

        private static void PrintStartupStatusMessages(IWebHost webHost)
        {
            var webHostEnvironment = webHost.Services.GetRequiredService<IWebHostEnvironment>();
            var addresses = webHost.ServerFeatures.Get<IServerAddressesFeature>().Addresses;

            Log.Information($"Hosting environment: {webHostEnvironment.EnvironmentName}");
            Log.Information($"Content root path: {webHostEnvironment.ContentRootPath}");

            foreach (var address in addresses)
            {
                Log.Information($"Now listening on: {address}");
            }

            Log.Information("Application started. Press Ctrl+C to shut down.");
        }
    }
}
