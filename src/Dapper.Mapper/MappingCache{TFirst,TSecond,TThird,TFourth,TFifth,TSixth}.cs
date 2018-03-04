// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System;
using System.Linq.Expressions;

namespace Dapper.Mapper
{
    internal static class MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(TFirst), "first");
            var second = Expression.Parameter(typeof(TSecond), "second");
            var third = Expression.Parameter(typeof(TThird), "third");
            var fourth = Expression.Parameter(typeof(TFourth), "fourth");
            var fifth = Expression.Parameter(typeof(TFifth), "fifth");
            var sixth = Expression.Parameter(typeof(TSixth), "sixth");

            var secondSetExpression = MappingCache.GetSetExpression(second, first);
            var thirdSetExpression = MappingCache.GetSetExpression(third, first, second);
            var fourthSetExpression = MappingCache.GetSetExpression(fourth, first, second, third);
            var fifthSetExpression = MappingCache.GetSetExpression(fifth, first, second, third, fourth);
            var sixthSetExpression = MappingCache.GetSetExpression(sixth, first, second, third, fourth, fifth);

            var blockExpression = Expression.Block(first, second, third, fourth, fifth, sixth, secondSetExpression, thirdSetExpression, fourthSetExpression, fifthSetExpression, sixthSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>>(blockExpression, first, second, third, fourth, fifth, sixth).Compile();
        }

        internal static Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst> Map { get; private set; }
    }
}
