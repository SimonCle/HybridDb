using System;
using System.Collections.Generic;
using HybridDb.Config;

namespace HybridDb
{
    public interface IDatabase : IDisposable
    {
        void Initialize();

        ManagedConnection Connect(bool schema = false);

        Dictionary<string, List<string>> QuerySchema();

        string Escape(string identifier);
        string FormatTableName(string tablename);
        string FormatTableNameAndEscape(string tablename);

        int RawExecute(string sql, object parameters = null, bool schema = false);
        IEnumerable<T> RawQuery<T>(string sql, object parameters = null, bool schema = false);
    }
}