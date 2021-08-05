using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(6)]
    public class Migration006_AddRoleScopeTable : Migration
    {
        private readonly string _schema;

        public Migration006_AddRoleScopeTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Create.Table("role_scope").InSchema(_schema)
                .WithColumn("r_id").AsInt64().NotNullable().ForeignKey("fk_role_scope_role", _schema, "role", "r_id")
                .WithColumn("s_id").AsString().NotNullable().ForeignKey("fk_role_scope_scope", _schema, "scope", "s_id");

            Create.PrimaryKey("role_scope_pk")
                .OnTable("role_scope").WithSchema(_schema)
                .Columns("r_id", "s_id");
        }

        public override void Down()
        {
            Delete.Table("role_scope").InSchema(_schema);
        }
    }
}
