using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Postgres;

namespace AccountService.Infrastructure.Migrations
{
    [Migration(20230615182800, "Initial of main tables")]
    public class InitialTables_20230615182800 : Migration
    {
        public override void Down()
        {
            Delete.Table("user");
            Delete.Table("subscription");
            Delete.Table("notification");
        }

        public override void Up()
        {
            Create.Table("user")
                .WithColumn("id").AsString(30).NotNullable().PrimaryKey()
                .WithColumn("name").AsString(30).NotNullable()
                .WithColumn("password").AsString(40).NotNullable()
                .WithColumn("email").AsString(30).NotNullable()
                .WithColumn("role").AsInt16().NotNullable();

            Create.Table("subscription")
                .WithColumn("user_id").AsString(30).NotNullable().PrimaryKey().ForeignKey("user", "id")
                .WithColumn("subscriber_id").AsString(30).NotNullable().PrimaryKey().ForeignKey("user", "id");
            
            Create.Table("notification")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(PostgresGenerationType.Always)
                .WithColumn("HtmlText").AsString().NotNullable()
                .WithColumn("IsChecked").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("UserId").AsString(30).NotNullable().ForeignKey("user", "id");

        }
    }
}
