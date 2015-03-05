using System.Linq;
using HybridDb.Schema;

namespace HybridDb.Migration.Commands
{
    public class CreateTable : SchemaMigrationCommand
    {
        public CreateTable(Table table)
        {
            Table = table;
        }

        public Table Table { get; private set; }

        public override void Execute(Database db)
        {
            var columns = Table.Columns.Select(col => string.Format("{0} {1}", col.Name, GetColumnSqlType(col)));

            var escaptedColumns =
                from column in columns
                let split = column.Split(' ')
                let name = split.First()
                let type = string.Join(" ", split.Skip(1))
                select db.Escape(name) + " " + type;

            db.RawExecute(
                string.Format("if not ({0}) begin create table {1} ({2}); end",
                    GetTableExistsSql(db, Table.Name),
                    db.FormatTableNameAndEscape(Table.Name),
                    string.Join(", ", escaptedColumns)));
        }
    }
}