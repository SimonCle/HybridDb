﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using HybridDb.Config;

namespace HybridDb.Commands
{
    public abstract class DatabaseCommand
    {
        internal abstract PreparedDatabaseCommand Prepare(DocumentStore store, Guid etag, int uniqueParameterIdentifier);

        protected static IDictionary<Column, object> ConvertAnonymousToProjections(Table table, object projections)
        {
            return projections as IDictionary<Column, object> ??
                   (from projection in projections as IDictionary<string, object> ?? ObjectToDictionaryRegistry.Convert(projections)
                    let column = table[projection]
                    where column != null
                    select new KeyValuePair<Column, object>(column, projection.Value)).ToDictionary();
        }

        protected static Dictionary<string, Parameter> MapProjectionsToParameters(IDictionary<Column, object> projections, int i)
        {
            var parameters = new Dictionary<string, Parameter>();
            foreach (var projection in projections)
            {
                var column = projection.Key;
                var sqlColumn = SqlTypeMap.Convert(column);
                AddTo(parameters, "@" + column.Name + i, projection.Value, sqlColumn.DbType, sqlColumn.Length);
            }

            return parameters;
        }

        public static void AddTo(Dictionary<string, Parameter> parameters, string name, object value, DbType? dbType, int? size)
        {
            if (dbType.Value == DbType.Xml)
            {
                var ms = new MemoryStream();
                var writer = XmlWriter.Create(ms);
                new XmlSerializer(value.GetType()).Serialize(writer, value);
                value = new SqlXml(ms);
            }

            parameters[name] = new Parameter {Name = name, Value = value, DbType = dbType, Size = size};
        }

        public class PreparedDatabaseCommand
        {
            public string Sql { get; set; }
            public List<Parameter> Parameters { get; set; }
            public int ExpectedRowCount { get; set; }
        }
    }
}