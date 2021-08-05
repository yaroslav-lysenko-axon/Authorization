using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(10)]
    public class Migration010_PopulateScopeTable : Migration
    {
        private readonly string _schema;

        public Migration010_PopulateScopeTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Insert.IntoTable("scope").InSchema(_schema)
                .Row(new { s_id = "scope-get-own-profile" });
        }

        public override void Down()
        {
            Delete.FromTable("scope").InSchema(_schema).AllRows();
        }
    }
}
