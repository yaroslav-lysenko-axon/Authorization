using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(3)]
    public class Migration003_AddClientTable : Migration
    {
        private readonly string _schema;

        public Migration003_AddClientTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Create.Table("client").InSchema(_schema)
                .WithColumn("c_id").AsInt64().NotNullable().PrimaryKey("client_pk").Identity()
                .WithColumn("secret").AsGuid().NotNullable().Unique()
                .WithColumn("name").AsString().NotNullable().Unique()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
        }

        public override void Down()
        {
            Delete.Table("client").InSchema(_schema);
        }
    }
}
