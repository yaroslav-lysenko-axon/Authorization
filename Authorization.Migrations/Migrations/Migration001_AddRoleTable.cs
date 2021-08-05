using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(1)]
    public class Migration001_AddRoleTable : Migration
    {
        private readonly string _schema;

        public Migration001_AddRoleTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Create.Schema(_schema);

            Create.Table("role").InSchema(_schema)
                .WithColumn("r_id").AsInt64().NotNullable().PrimaryKey("role_pk").Identity()
                .WithColumn("name").AsString().NotNullable().Unique();
        }

        public override void Down()
        {
            Delete.Table("Role").InSchema(_schema);
        }
    }
}
