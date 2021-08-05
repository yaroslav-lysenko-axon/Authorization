using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(4)]
    public class Migration004_AddRefreshTokenTable : Migration
    {
        private readonly string _schema;

        public Migration004_AddRefreshTokenTable(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Create.Table("refresh_token").InSchema(_schema)
                .WithColumn("rt_id").AsInt64().NotNullable().PrimaryKey("refresh_token_pk").Identity()
                .WithColumn("c_id").AsInt64().NotNullable().ForeignKey("fk_refresh_token_client", _schema, "client", "c_id")
                .WithColumn("u_id").AsInt64().NotNullable().ForeignKey("fk_refresh_token_user", _schema, "user", "u_id")
                .WithColumn("refresh_token").AsString().NotNullable()
                .WithColumn("expire_at").AsDateTime().NotNullable()
                .WithColumn("revoked_at").AsDateTime().Nullable()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("revoke_reason").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("refresh_token").InSchema(_schema);
        }
    }
}
