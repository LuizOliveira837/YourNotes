using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace YourNotes.Persistence.Data.Migrations.Tables
{
    public abstract class BaseTableAttributes : ForwardOnlyMigration
    {

        public ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName)
        {
            return Create
               .Table(tableName)
               .WithColumn("Id").AsGuid().PrimaryKey()
               .WithColumn("Active").AsBoolean().WithDefaultValue(1)
               .WithColumn("CreatedOn").AsDateTime().NotNullable()
               .WithColumn("UpdatedOn").AsDateTime().NotNullable();
        }
    }
}
