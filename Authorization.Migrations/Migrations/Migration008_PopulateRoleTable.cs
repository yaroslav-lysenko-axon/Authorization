using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(8)]
    public class Migration008_PopulateRoleTable : Migration
    {
        private readonly string _schema;

        public Migration008_PopulateRoleTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Insert.IntoTable("role").InSchema(_schema)
                .Row(new { r_id = 1, name = "USER" })
                .Row(new { r_id = 2, name = "UNCONFIRMED_USER" });
        }

        public override void Down()
        {
            Delete.FromTable("role").InSchema(_schema).AllRows();
        }
    }
}
