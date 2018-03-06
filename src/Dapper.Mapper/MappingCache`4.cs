// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System;
using System.Linq.Expressions;

namespace Dapper.Mapper
{
    internal static class MappingCache<TFirst, TSecond, TThird, TFourth>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(TFirst), "first");
            var second = Expression.Parameter(typeof(TSecond), "second");
            var third = Expression.Parameter(typeof(TThird), "third");
            var fourth = Expression.Parameter(typeof(TFourth), "fourth");

            var secondSetExpression = MappingCache.GetSetExpression(second, first);
            var thirdSetExpression = MappingCache.GetSetExpression(third, first, second);
            var fourthSetExpression = MappingCache.GetSetExpression(fourth, first, second, third);

            var blockExpression = Expression.Block(first, second, third, fourth, secondSetExpression, thirdSetExpression, fourthSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TThird, TFourth, TFirst>>(blockExpression, first, second, third, fourth).Compile();
        }

        internal static Func<TFirst, TSecond, TThird, TFourth, TFirst> Map { get; private set; }
    }
}
