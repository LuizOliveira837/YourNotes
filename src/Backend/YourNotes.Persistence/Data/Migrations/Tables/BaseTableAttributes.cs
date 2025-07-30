using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
