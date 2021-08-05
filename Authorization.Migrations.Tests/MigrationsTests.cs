using System.Linq;
using System.Reflection;
using Authorization.Migrations.Migrations;
using FluentMigrator;
using FluentMigrator.Infrastructure.Extensions;
using Xunit;

namespace Authorization.Migrations.Tests
{
    public class MigrationsTests
    {
        [Fact]
        public void MigrationsHaveNoDuplicateOrders()
        {
            // Arrange
            var assembly = typeof(Migration001_AddRoleTable).Assembly;
            var migrations = assembly.GetTypes()
                .Where(x => x.IsClass && x.IsSubclassOf(typeof(Migration)) && x.HasAttribute<MigrationAttribute>())
                .ToArray();

            // Act
            var migrationsWithOrders = migrations.GroupBy(x => x.GetCustomAttribute<MigrationAttribute>().Version);

            // Assert
            foreach (var kvp in migrationsWithOrders)
            {
                var order = kvp.Key;
                var migrationsWithThisOrder = kvp.ToArray();

                Assert.True(migrationsWithThisOrder.Length == 1, $"Multiple migrations with order {order} detected.");
            }
        }
    }
}
