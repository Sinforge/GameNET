using FluentMigrator;
using FluentMigrator.Postgres;
namespace ChatService.Infrastructure.Migrations
{
    [Migration(202307021410)]
    public class InitialTables_202307021410 : Migration
    {
        public override void Down()
        {
            Delete.Table("game");
            Delete.Table("tag");
            Delete.Table("game_tag");
            Delete.Table("chat");
        }

        public override void Up()
        {
            Create.Table("game")
               .WithColumn("id").AsGuid().Unique().NotNullable().PrimaryKey()
               .WithColumn("name").AsString(30).NotNullable();
               //.WithColumn("img_name").AsString(40).NotNullable();

            //               .WithColumn("tag_id").AsInt16().NotNullable().ForeignKey("tag", "id")
            Create.Table("tag")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity(PostgresGenerationType.Always)
                .WithColumn("name").AsString(30).NotNullable();
            Create.Table("game_tag")
                .WithColumn("game_id").AsGuid().ForeignKey("game", "id")
                .WithColumn("tag_id").AsInt32().NotNullable().ForeignKey("tag", "id");
            Create.Table("chat")
                .WithColumn("id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("title").AsString(50).NotNullable()
                .WithColumn("game_id").AsGuid().NotNullable().ForeignKey("game", "id");
        }
    }
}
