﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using HybridDb.Config;
using HybridDb.Migration;
using Serilog;

namespace HybridDb.MigrationRunner
{
    internal class Runner
    {
//        readonly DocumentStore store;
//        readonly ILogger filelog;
//        readonly List<WriteThroughLogger.Entry> recentLogs;
//        readonly WriteThroughLogger logger;
//        int numberOfMigrations;

//        [Import(typeof(Migration.Migration), AllowRecomposition = true, AllowDefault = true)]
//        public Migration.Migration Migration { get; set; }

//        public Runner(DocumentStore store)
//        {
//            this.store = store;

//            filelog = new LoggerConfiguration()
//                .WriteTo.File("MigrationRunner.log")
//                .CreateLogger();

//            recentLogs = new List<WriteThroughLogger.Entry>();

//            logger = new WriteThroughLogger(entry =>
//            {
//                filelog.Information(entry.Message, entry.Args);
//                recentLogs.Add(entry);
//            });

//            store.Configuration.UseLogger(logger);

//            numberOfMigrations = 0;
//        }

//        public void Run()
//        {
//            Console.Clear();
//            Console.ForegroundColor = ConsoleColor.White;
//            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight < 50 ? Console.LargestWindowHeight : 50);

//            recentLogs.Clear();

//            if (Migration == null)
//                return;

//            numberOfMigrations = Migration.DocumentMigrations.Count;

//            if (!MigrateSchema(Migration)) return;
//            MigrateDocuments(Migration);
//        }

//        bool MigrateSchema(Migration.Migration migration)
//        {
//            var schemaMigration = migration.SchemaMigration;
//            if (schemaMigration != null)
//            {
//                using (var schemaMigrator = new SchemaMigrator(store))
//                {
//                    UpdateConsole(migration, "Migrating schema");

//                    try
//                    {
////                        schemaMigration.Migration(schemaMigrator);
//                    }
//                    catch (Exception ex)
//                    {
//                        logger.Error("Schema migration failed", ex);
//                        UpdateConsole(migration, "Schema migration failed");
//                        Pause();
//                        return false;
//                    }

//                    UpdateConsole(migration, "Committing schema migration");
//                    schemaMigrator.Commit();
//                }

//                UpdateConsole(migration, null);
//            }
//            return true;
//        }

//        void MigrateDocuments(Migration.Migration migration)
//        {
//            foreach (var documentMigrations in migration.DocumentMigrations.OrderBy(x => x.Version).GroupBy(x => x.Tablename))
//            {
//                var migrator = new DocumentMigrator();

//                if (documentMigrations.Key == null)
//                    throw new ArgumentException("Document migration must have a tablename");

//                var table = new DocumentTable(documentMigrations.Key);
//                while (true)
//                {
//                    PauseOnSpacebar();

//                    QueryStats stats;

//                    if (documentMigrations.First().Version == 0)
//                        throw new ArgumentException("Document migration must have a version number larger than 0");
                    
//                    var @where = String.Format("Version < {0}", documentMigrations.First().Version);

//                    UpdateConsole(migration, null);

//                    var rows = store.Query(table, out stats, @where: @where, take: 100);

//                    UpdateConsole(migration, stats.TotalResults.ToString());

//                    if (stats.TotalResults == 0)
//                        break;

//                    foreach (var row in rows.Select(x => x.ToDictionary(col => col.Key, col => col.Value)))
//                    {
//                        var id = (Guid) row[table.IdColumn.Name];
//                        var etag = (Guid) row[table.EtagColumn.Name];

//                        foreach (var documentMigration in documentMigrations)
//                        {
//                            migrator.OnRead(documentMigration, table, row);
//                        }

//                        try
//                        {
//                            store.Update(table, id, etag, row);
//                        }
//                        catch (ConcurrencyException)
//                        {
//                            // We don't care. Either the version is bumped by other user or we'll retry in next round.
//                        }
//                    }
//                }
//            }
//        }

//        static void PauseOnSpacebar()
//        {
//            if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
//            {
//                Pause();
//            }
//        }

//        static void Pause()
//        {
//            Console.SetCursorPosition(Console.WindowWidth - 6, Console.WindowHeight - 2);
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.Write("PAUSED");
//            Console.ForegroundColor = ConsoleColor.White;

//            while (true)
//            {
//                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
//                {
//                    Console.Clear();
//                    break;
//                }
//                Thread.Sleep(100);
//            }
//        }

//        void UpdateConsole(Migration.Migration migration, string status)
//        {
//            Console.SetCursorPosition(0, 0);
//            Console.Write(migration.GetType().Name);
//            WriteQueue();
//            WriteLog();
//            WriteStatus(status);
//        }

//        static void WriteStatus(string status)
//        {
//            if (status == null)
//                return;

//            Console.SetCursorPosition(0, 1);
//            Console.SetCursorPosition(Console.WindowWidth - 30, 0);
//            Console.Write(status.PadLeft(30));
//        }

//        void WriteQueue()
//        {
//            if (numberOfMigrations > 1)
//            {
//                Console.SetCursorPosition(0, 3);
//                Console.WriteLine("Migration queue:");
//                foreach (var queuedMigration in Migration.DocumentMigrations.Skip(1))
//                {
//                    Console.Write(queuedMigration.GetType().Name);
//                    ClearRestOfLine();
//                }
//            }
//        }

//        void WriteLog()
//        {
//            var top = numberOfMigrations + 4;
//            Console.SetCursorPosition(0, top);
//            Console.WriteLine("Log output:");
//            var linesLeftInWindow = (Console.WindowHeight - (top + 2));
//            foreach (var log in recentLogs.Skip(recentLogs.Count - linesLeftInWindow))
//            {
//                var levelString = log.Level.ToString().ToUpperInvariant();

//                Console.Write("{0} ({1}): {2}",
//                              levelString,
//                              Thread.CurrentThread.Name,
//                              String.Format(log.Message, log.Args));
//                ClearRestOfLine();
//            }
//        }

//        void ClearRestOfLine()
//        {
//            Console.Write(new string(' ', Console.WindowWidth) + "\r");
//        }
    }
}