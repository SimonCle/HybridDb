﻿using System;
using System.Collections.Generic;
using HybridDb.Commands;

namespace HybridDb
{
    public interface IAdvancedDocumentSession
    {
        void Clear();
        bool IsLoaded(Guid id);
        IDocumentStore DocumentStore { get; }
        void Defer(DatabaseCommand command);
        void Evict(object entity);
        Guid? GetEtagFor(object entity);
        void SaveChangesLastWriterWins();
        IEnumerable<object> ManagedEntities { get; }
    }
}