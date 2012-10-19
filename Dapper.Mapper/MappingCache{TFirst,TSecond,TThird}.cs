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

            var secondParameterAndPropery = MappingCache.GetParameterAndProperty(typeof(TSecond), first);
            var thirdParameterAndPropery = MappingCache.GetParameterAndProperty(typeof(TThird), first, second);

            var secondSetExpression = Expression.IfThen(
                Expression.Not(Expression.Equal(secondParameterAndPropery.Item1, Expression.Constant(null))),
                Expression.Call(secondParameterAndPropery.Item1, secondParameterAndPropery.Item2.GetSetMethod(), second));

            var thirdSetExpression = Expression.IfThen(
                Expression.Not(Expression.Equal(thirdParameterAndPropery.Item1, Expression.Constant(null))),
                Expression.Call(thirdParameterAndPropery.Item1, thirdParameterAndPropery.Item2.GetSetMethod(), third));

            var blockExpression = Expression.Block(first, second, third, secondSetExpression, thirdSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TThird, TFirst>>(blockExpression, first, second, third).Compile();
        }

        internal static Func<TFirst, TSecond, TThird, TFirst> Map { get; private set; }
    }
}
