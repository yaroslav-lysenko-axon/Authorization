using System;
using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using Authorization.Migrations.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Authorization.Migrations
{
    public sealed class Program
    {
        private const string PersistenceSectionName = "Persistence";

        private static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            Log.Debug("Reading configuration...");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            Log.Debug("Binding configuration...");

            var persistenceConfiguration = new PersistenceConfiguration();
            configuration.GetSection(PersistenceSectionName).Bind(persistenceConfiguration);
            var connectionString = persistenceConfiguration.GetConnectionString();

            Log.Information($"Connection string: {connectionString}");

            Log.Debug("Registering services...");

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPersistenceConfiguration>(persistenceConfiguration)
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Migration001_AddRoleTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            Log.Information("Migrator initialized!");

            return serviceProvider;
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            Log.Information("Starting migration...");

            runner.MigrateUp();

            Log.Information("Migration finished!");
        }
    }
}
