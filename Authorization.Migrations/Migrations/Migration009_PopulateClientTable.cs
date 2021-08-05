using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(9)]
    public class Migration009_PopulateClientTable : Migration
    {
        private readonly string _schema;

        public Migration009_PopulateClientTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Insert.IntoTable("client").InSchema(_schema)
                .Row(new
                {
                    c_id = 1,
                    secret = "edd67720-9c02-11ea-bb37-0242ac130002",
                    name = "WEB",
                    created_at = "2020-05-06 18:28:44",
                });
        }

        public override void Down()
        {
            Delete.FromTable("client").InSchema(_schema).AllRows();
        }
    }
}
