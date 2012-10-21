/*
 Copyright © 2012 Arjen Post (http://arjenpost.nl)
 License: http://www.apache.org/licenses/LICENSE-2.0 
 */

namespace Dapper.Mapper
{
    using System;
    using System.Linq.Expressions;

    internal static class MappingCache<TFirst, TSecond, TThird>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(TFirst), "first");
            var second = Expression.Parameter(typeof(TSecond), "second");
            var third = Expression.Parameter(typeof(TThird), "third");

            var secondSetExpression = MappingCache.GetSetExpression(second, first);
            var thirdSetExpression = MappingCache.GetSetExpression(third, first, second);

            var blockExpression = Expression.Block(first, second, third, secondSetExpression, thirdSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TThird, TFirst>>(blockExpression, first, second, third).Compile();
        }

        internal static Func<TFirst, TSecond, TThird, TFirst> Map { get; private set; }
    }
}
