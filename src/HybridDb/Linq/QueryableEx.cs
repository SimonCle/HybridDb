using System;
using System.Linq;
using System.Linq.Expressions;
using HybridDb.Config;
using HybridDb.Linq.Builders;

namespace HybridDb.Linq
{
    public static class QueryableEx
    {
        public static IQueryable<T> AsProjection<T>(this IQueryable query)
        {
            return query.Provider.CreateQuery<T>(query.OfType<T>().Expression);
        }

        public static IQueryable<T> Statistics<T>(this IQueryable<T> query, out QueryStats stats) where T : class
        {
            ((QueryProvider<T>)query.Provider).WriteStatisticsTo(out stats);
            return query;
        }

        public static bool In<T>(this T property, params T[] list)
        {
            return list.Contains(property);
        }
        
        public static T Column<T>(this object parameter, string name)
        {
            throw new NotSupportedException("Only for building LINQ expressions");
        }
        
        public static T Index<T>(this object parameter)
        {
            throw new NotSupportedException("Only for building LINQ expressions");
        }
        
        internal static SqlQuery Translate(this IQueryable query)
        {
            return Translate(query.Expression, null);
        }

        internal static SqlQuery Translate(this Expression expression, Configuration configuration)
        {
            return new SqlQueryEmitter().Translate(configuration, expression);
        }
    }
}