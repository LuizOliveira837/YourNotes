using FluentMigrator;

namespace YourNotes.Persistence.Data.Migrations.Tables
{
    [Migration(Versions.TABLE_USER, "Create table to save the user's information")]
    public class Version00000001 : BaseTableAttributes
    {
        public override void Up()
        {
            CreateTable("Users")
                .WithColumn("FirstName").AsAnsiString().Nullable()
                .WithColumn("LastName").AsAnsiString().Nullable()
                .WithColumn("UserName").AsAnsiString().Unique().Nullable()
                .WithColumn("Email").AsAnsiString().Unique().Nullable()
                .WithColumn("Password").AsAnsiString().Nullable();

        }
    }
}
