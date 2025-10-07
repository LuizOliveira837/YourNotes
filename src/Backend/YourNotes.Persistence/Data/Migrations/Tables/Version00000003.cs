using FluentMigrator;

namespace YourNotes.Persistence.Data.Migrations.Tables
{
    [Migration(Versions.TABLE_CONTENT, "Create table to save the contents of topic")]
    public class Version00000003 : BaseTableAttributes
    {
        public override void Up()
        {
            CreateTable("Contents")
                .WithColumn("ArticleId").AsGuid().NotNullable().ForeignKey("FK_Content_Article_Id", "Articles", "Id")
                .WithColumn("Markup").AsString(int.MaxValue).NotNullable()
                .WithColumn("Position").AsInt64().NotNullable();
        }
    }
}
