// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System;
using System.Linq.Expressions;

namespace Dapper.Mapper
{
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
