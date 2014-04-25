using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

/// <summary>
/// Summary description for Hotels2Comparable
/// </summary>
/// 
namespace Hotels2thailand
{
    public static class Hotels2Comparable
    {
        //public static bool Between<T>(this T source, T low, T high) where T : IComparable
        //{
        //    return source.CompareTo(low) >= 0 && source.CompareTo(high) <= 0;
        //}


        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            Expression lowerBound = Expression.LessThanOrEqual(Expression.Constant(low), key);
            Expression upperBound = Expression.LessThanOrEqual(key, Expression.Constant(high));
            Expression and = Expression.AndAlso(lowerBound, upperBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);
            return source.Where(lambda);
        }
    }
}