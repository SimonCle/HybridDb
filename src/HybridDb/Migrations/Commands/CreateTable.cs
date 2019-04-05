using System;
using System.Linq;
using HybridDb.Config;

namespace HybridDb.Migrations.Commands
{
    public class CreateTable : SchemaMigrationCommand
    {
        public CreateTable(Table table) => Table = table;

        public Table Table { get; }

        public override string ToString() => $"Create table {Table} with columns {string.Join(", ", Table.Columns.Select(x => x.ToString()))}";
    }

    public class CreateTableExecutor : DdlCommandExecutor<DocumentStore, CreateTable>
    {
        public override void Execute(DocumentStore store, CreateTable command)
        {
            if (!command.Table.Columns.Any())
            {
                throw new InvalidOperationException("Cannot create a table with no columns.");
            }

            var sql = new SqlBuilder()
                .Append($"if not ({store.BuildTableExistsSql(command.Table.Name)})")
                .Append($"begin create table {store.Database.FormatTableNameAndEscape(command.Table.Name)} (");

            foreach (var (column, i) in command.Table.Columns.Select((column, i) => (column, i)))
            {
                sql.Append(new SqlBuilder()
                    .Append(i > 0, ",")
                    .Append(store.Database.Escape(column.Name))
                    .Append(store.BuildColumnSql(column)));
            }

            sql.Append(") end;");

            store.Database.RawExecute(sql.ToString(), schema: true);
        }
    }
}