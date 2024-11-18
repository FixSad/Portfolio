using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;

namespace BuildYourself.Domain.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> WhereIf(this IQueryable<T> source, bool condition,
            Expression<Func<T, bool>> predicate)
        {
            if(condition)
                return source.Where(predicate);
            return source;
        }
    }
}
