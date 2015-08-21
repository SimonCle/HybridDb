﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HybridDb.Config;

namespace HybridDb.Linq
{
    public class QueryProvider<TSourceElement> : IHybridQueryProvider where TSourceElement : class
    {
        readonly IDocumentStore store;
        readonly DocumentSession session;
        readonly DocumentDesign design;
        readonly QueryStats lastQueryStats;

        public QueryProvider(DocumentSession session, DocumentDesign design)
        {
            this.session = session;
            this.design = design;
            store = session.Advanced.DocumentStore;
            lastQueryStats = new QueryStats();
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new Query<T>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            var elementType = expression.Type.GetEnumeratedType();
            if (elementType == null)
            {
                throw new ArgumentException("Query is not based on IEnumerable");
            }

            try
            {
                return (IQueryable) Activator.CreateInstance(typeof (Query<>).MakeGenericType(elementType), new object[] {this, expression});
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        public IEnumerable<TProjection> ExecuteEnumerable<TProjection>(Expression expression)
        {
            return ExecuteQuery<TProjection>(expression).Results;
        }

        T IQueryProvider.Execute<T>(Expression expression)
        {
            var result = ExecuteQuery<T>(expression);

            switch (result.SqlQuery.ExecutionMethod)
            {
                case SqlQuery.ExecutionSemantics.Single:
                    if (lastQueryStats.TotalResults > 1)
                        throw new InvalidOperationException("Query returned more than one element");

                    if (lastQueryStats.TotalResults < 1)
                        throw new InvalidOperationException("Query returned no elements");

                    return result.Results.Single();
                case SqlQuery.ExecutionSemantics.SingleOrDefault:
                    if (lastQueryStats.TotalResults > 1)
                        throw new InvalidOperationException("Query returned more than one element");

                    if (lastQueryStats.TotalResults < 1)
                        return default(T);

                    return result.Results.Single();
                case SqlQuery.ExecutionSemantics.First:
                    if (lastQueryStats.TotalResults < 1)
                        throw new InvalidOperationException("Query returned no elements");

                    return result.Results.First();
                case SqlQuery.ExecutionSemantics.FirstOrDefault:
                    if (lastQueryStats.TotalResults < 1)
                        return default(T);

                    return result.Results.First();
                default:
                    throw new ArgumentOutOfRangeException("Does not support execution method " + result.SqlQuery.ExecutionMethod);
            }
        }

        TranslationAndResult<TProjection> ExecuteQuery<TProjection>(Expression expression)
        {
            var translation = expression.Translate(store.Configuration);

            Debug.WriteLine(translation.Where);

            if (typeof (TProjection).IsAssignableFrom(design.DocumentType))
            {
                QueryStats storeStats;
                var results = store.Query(
                    design.Table, out storeStats,
                    translation.Select,
                    translation.Where,
                    translation.Skip,
                    translation.Take,
                    translation.OrderBy,
                    translation.Parameters)
                    .Select(result => session.ConvertToEntityAndPutUnderManagement(design, result))
                    .Where(result => result != null)
                    .Cast<TProjection>();
  
                storeStats.CopyTo(lastQueryStats);
                return new TranslationAndResult<TProjection>(translation, results);
            }
            else
            {
                var table = design.Table;

                QueryStats storeStats;
                var results = store.Query<TProjection>(
                    table, out storeStats,
                    translation.Select,
                    translation.Where,
                    translation.Skip,
                    translation.Take,
                    translation.OrderBy,
                    translation.Parameters);

                storeStats.CopyTo(lastQueryStats);
                return new TranslationAndResult<TProjection>(translation, results);
            }
        }

        public string GetQueryText(IQueryable query)
        {
            return query.Translate().Where;
        }

        internal void WriteStatisticsTo(out QueryStats stats)
        {
            stats = lastQueryStats;
        }

        public object Execute(Expression expression)
        {
            throw new NotSupportedException();
        }

        class TranslationAndResult<T>
        {
            public TranslationAndResult(SqlQuery sqlQuery, IEnumerable<T> results)
            {
                SqlQuery = sqlQuery;
                Results = results;
            }

            public SqlQuery SqlQuery { get; private set; }
            public IEnumerable<T> Results { get; private set; }
        }
    }
}