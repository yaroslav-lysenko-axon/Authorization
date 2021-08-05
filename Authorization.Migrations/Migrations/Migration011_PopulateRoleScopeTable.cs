using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(11)]
    public class Migration011_PopulateRoleScopeTable : Migration
    {
        private readonly string _schema;

        public Migration011_PopulateRoleScopeTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Insert.IntoTable("role_scope").InSchema(_schema)
                .Row(new { r_id = 1, s_id = "scope-get-own-profile" });
        }

        public override void Down()
        {
            Delete.FromTable("role_scope").InSchema(_schema).AllRows();
        }
    }
}
