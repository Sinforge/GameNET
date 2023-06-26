using FluentMigrator;
using FluentMigrator.Postgres;

namespace ArticleService.Migrations
{
    [Migration(202306152239)]
    public class InitialTables_202306152239 : Migration
    {
        public override void Down()
        {
            Delete.Table("article");
            Delete.Table("comment");
        }

        public override void Up()
        {
            Create.Table("article")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity(PostgresGenerationType.Always)
                .WithColumn("title").AsString(40).NotNullable()
                .WithColumn("text").AsString().NotNullable()
                .WithColumn("owner").AsString(30).NotNullable();

            Create.Table("comment")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity(PostgresGenerationType.Always)
                .WithColumn("reputation").AsInt16().NotNullable().WithDefaultValue(0)
                .WithColumn("date").AsDateTime().NotNullable()
                .WithColumn("content").AsString(30).NotNullable()
                .WithColumn("article_id").AsInt32().NotNullable().ForeignKey("article", "id")
                .WithColumn("parent_id").AsInt32().Nullable().WithDefaultValue(null).ForeignKey("comment", "id");
        }
    }
}
