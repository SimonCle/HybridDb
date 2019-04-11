﻿using HybridDb.Migrations.Schema;

namespace HybridDb.Events.Commands
{
    public class CreateEventTable : SchemaMigrationCommand
    {
        public EventTable EventTable { get; }

        public CreateEventTable(EventTable eventTable) => EventTable = eventTable;

        public override string ToString() => "Create event table";

        public static void CreateEventTableExecutor(DocumentStore store, CreateEventTable command)
        {
            var tableName = store.Database.FormatTableName(command.EventTable.Name);

            //store.Database.RawExecute($"ALTER DATABASE \"{store.Database}\" SET ALLOW_SNAPSHOT_ISOLATION ON;", schema: true);

            store.Database.RawExecute($@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{tableName}')
                BEGIN                       
                    CREATE TABLE [dbo].[{tableName}] (
	                    [Position] [bigint] NOT NULL IDENTITY(0,1),
                        [RowVersion] [rowversion] NOT NULL,
                        [EventId] [uniqueidentifier] NOT NULL,
	                    [CommitId] [uniqueidentifier] NOT NULL,
	                    [StreamId] [nvarchar](850) NOT NULL,
	                    [SequenceNumber] [bigint] NOT NULL,
                        [Name] [nvarchar](850) NOT NULL,
                        [Generation] [nvarchar](10) NOT NULL,
	                    [Metadata] [nvarchar](max) NULL,
	                    [Data] [varbinary](max) NULL,

                        CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED ([Position] ASC)
                    )

                    CREATE UNIQUE NONCLUSTERED INDEX [{tableName}_StreamId_SequenceNumber] ON [dbo].[{tableName}] ([StreamId] ASC, [SequenceNumber] ASC)

                    CREATE UNIQUE NONCLUSTERED INDEX [{tableName}_EventId] ON [dbo].[{tableName}] ([EventId])  

                    CREATE NONCLUSTERED INDEX [{tableName}_CommitId] ON [dbo].[{tableName}] ([CommitId])  

                END", schema: true);
        }
    }
}