using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(2)]
    public class Migration002_AddUserTable : Migration
    {
        private readonly string _schema;

        public Migration002_AddUserTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Create.Table("user").InSchema(_schema)
                .WithColumn("u_id").AsInt64().NotNullable().PrimaryKey("user_pk").Identity()
                .WithColumn("email").AsString(256).NotNullable().Unique()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("salt").AsString().NotNullable()
                .WithColumn("first_name").AsString().NotNullable()
                .WithColumn("last_name").AsString().NotNullable()
                .WithColumn("r_id").AsInt64().NotNullable().ForeignKey("user_role_id_fk", _schema, "role", "r_id")
                .WithColumn("active").AsBoolean().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("removed_at").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("User").InSchema(_schema);
        }
    }
}
