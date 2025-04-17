using System.Linq.Expressions;

namespace CaoHub.Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> query, 
            bool condition, 
            Expression<Func<T, bool>> predicate)
        {
            if (!condition)
            {
                return query;
            }

            return query.Where(predicate);
        }
    }
}
