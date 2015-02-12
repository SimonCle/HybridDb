﻿using System;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using HybridDb.Logging;
using HybridDb.Schema;

namespace HybridDb
{
    public interface IConfigurator
    {
        
    }

    public class Configuration
    {
        public Configuration(IDocumentStore store)
        {
            Store = store;
            
            Tables = new ConcurrentDictionary<string, Table>();
            DocumentDesigns = new ConcurrentDictionary<Type, DocumentDesign>();

            Serializer = new DefaultBsonSerializer();
            Logger = new ConsoleLogger(LogLevel.Info, new LoggingColors());

            var metadata = new Table("HybridDb");
            metadata.Register(new Column("Table", new SqlColumn(DbType.AnsiStringFixedLength, 255)));
            metadata.Register(new Column("SchemaVersion", new SqlColumn(DbType.Int32)));
            metadata.Register(new Column("DocumentVersion", new SqlColumn(DbType.Int32)));

            Tables.TryAdd(metadata.Name, metadata);
        }

        public IDocumentStore Store { get; set; }
        
        public ILogger Logger { get; private set; }
        public ISerializer Serializer { get; private set; }

        public ConcurrentDictionary<string, Table> Tables { get; private set; }
        public ConcurrentDictionary<Type, DocumentDesign> DocumentDesigns { get; private set; }

        public static string GetColumnNameByConventionFor(Expression projector)
        {
            var columnNameBuilder = new ColumnNameBuilder();
            columnNameBuilder.Visit(projector);
            return columnNameBuilder.ColumnName;
        }

        public string GetTableNameByConventionFor<TEntity>()
        {
            return Inflector.Inflector.Pluralize(typeof(TEntity).Name);
        }

        public DocumentDesign<TEntity> Document<TEntity>(string tablename)
        {
            tablename = tablename ?? GetTableNameByConventionFor<TEntity>();

            var baseDesign = DocumentDesigns
                .Where(x => x.Key.IsAssignableFrom(typeof (TEntity)))
                .Select(x => x.Value)
                .SingleOrDefault();
            
            if (baseDesign != null)
            {
                var design = new DocumentDesign<TEntity>(this, baseDesign);
                DocumentDesigns.TryAdd(design.Type, design);
                return design;
            }
            else
            {
                var table = new DocumentTable(tablename);
                Tables.TryAdd(tablename, table);

                var design = new DocumentDesign<TEntity>(this, table);
                DocumentDesigns.TryAdd(design.Type, design);
                return design;
            }
        }

        public DocumentDesign<T> GetDesignFor<T>()
        {
            return (DocumentDesign<T>) GetDesignFor(typeof(T));
        }

        public DocumentDesign GetDesignFor(Type type)
        {
            var design = TryGetDesignFor(type);
            if (design != null) return design;
            throw new TableNotFoundException(type);
        }

        public DocumentDesign<T> TryGetDesignFor<T>()
        {
            return (DocumentDesign<T>) TryGetDesignFor(typeof(T));
        }

        public DocumentDesign TryGetDesignFor(Type type)
        {
            DocumentDesign table;
            return DocumentDesigns.TryGetValue(type, out table) ? table : null;
        }

        public DocumentDesign TryGetDesignFor(string tablename)
        {
            return DocumentDesigns.Values.SingleOrDefault(x => x.Table.Name == tablename);
        }

        public IndexTable TryGetIndexTableByName(string tablename)
        {
            Table table;
            return Tables.TryGetValue(tablename, out table) ? table as IndexTable : null;
        }

        public void UseSerializer(ISerializer serializer)
        {
            Serializer = serializer;
        }

        public void UseLogger(ILogger logger)
        {
            Logger = logger;
        }
    }
}