using FluentMigrator;

namespace YourNotes.Persistence.Data.Migrations.Tables
{
    [Migration(Versions.TABLE_TOPIC, "Create table to save the topic's information")]
    public class Version00000002 : BaseTableAttributes
    {
        public override void Up()
        {
            CreateTable("Topics")
                .WithColumn("Title").AsAnsiString().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_Topic_User_Id", "Users", "Id");

            CreateTable("Articles")
                .WithColumn("TopicId").AsGuid().NotNullable().ForeignKey("FK_Article_Topic_Id", "Topics", "Id")
                .WithColumn("Title").AsAnsiString(100).NotNullable()
                .WithColumn("Description").AsAnsiString(100)
                .WithColumn("PublishOn").AsBoolean().NotNullable();



        }
    }
}
