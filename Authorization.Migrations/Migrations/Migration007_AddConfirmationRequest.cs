using Authorization.Infrastructure.Persistence.ConfigurationClasses;
using FluentMigrator;

namespace Authorization.Migrations.Migrations
{
    [Migration(7)]
    public class Migration007_AddConfirmationRequest : Migration
    {
        private readonly string _schema;

        public Migration007_AddConfirmationRequest(IPersistenceConfiguration configuration)
        {
            _schema = configuration.Schema;
        }

        public override void Up()
        {
            Create.Table("confirmation_request").InSchema(_schema)
                .WithColumn("cr_id").AsInt64().NotNullable().PrimaryKey("confirmation_request_pk").Identity()
                .WithColumn("u_id").AsInt64().NotNullable().ForeignKey("confirmation_request_user_fk", _schema, "user", "u_id")
                .WithColumn("key").AsString().NotNullable().Unique()
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("expired_at").AsDateTime().Nullable()
                .WithColumn("confirmed").AsBoolean().NotNullable()
                .WithColumn("request_type").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("confirmation_request").InSchema(_schema);
        }
    }
}
