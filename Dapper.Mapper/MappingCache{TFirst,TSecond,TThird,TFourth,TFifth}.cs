/*
 Copyright © 2012 Arjen Post (http://arjenpost.nl)
 License: http://www.apache.org/licenses/LICENSE-2.0 
 */

namespace Dapper.Mapper
{
    using System;
    using System.Linq.Expressions;

    internal static class MappingCache<TFirst, TSecond, TThird, TFourth, TFifth>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(TFirst), "first");
            var second = Expression.Parameter(typeof(TSecond), "second");
            var third = Expression.Parameter(typeof(TThird), "third");
            var fourth = Expression.Parameter(typeof(TFourth), "fourth");
            var fifth = Expression.Parameter(typeof(TFifth), "fifth");

            var secondParameterAndPropery = MappingCache.GetParameterAndProperty(typeof(TSecond), first);
            var thirdParameterAndPropery = MappingCache.GetParameterAndProperty(typeof(TThird), first, second);
            var fourthParameterAndPropery = MappingCache.GetParameterAndProperty(typeof(TFourth), first, second, third);
            var fifthParameterAndPropery = MappingCache.GetParameterAndProperty(typeof(TFifth), first, second, third, fourth);

            var secondSetExpression = Expression.IfThen(
                Expression.Not(Expression.Equal(secondParameterAndPropery.Item1, Expression.Constant(null))),
                Expression.Call(secondParameterAndPropery.Item1, secondParameterAndPropery.Item2.GetSetMethod(), second));

            var thirdSetExpression = Expression.IfThen(
                Expression.Not(Expression.Equal(thirdParameterAndPropery.Item1, Expression.Constant(null))),
                Expression.Call(thirdParameterAndPropery.Item1, thirdParameterAndPropery.Item2.GetSetMethod(), third));

            var fourthSetExpression = Expression.IfThen(
                Expression.Not(Expression.Equal(fourthParameterAndPropery.Item1, Expression.Constant(null))),
                Expression.Call(fourthParameterAndPropery.Item1, fourthParameterAndPropery.Item2.GetSetMethod(), fourth));

            var fifthSetExpression = Expression.IfThen(
                Expression.Not(Expression.Equal(fifthParameterAndPropery.Item1, Expression.Constant(null))),
                Expression.Call(fifthParameterAndPropery.Item1, fifthParameterAndPropery.Item2.GetSetMethod(), fifth));

            var blockExpression = Expression.Block(first, second, third, fourth, fifth, secondSetExpression, thirdSetExpression, fourthSetExpression, fifthSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>>(blockExpression, first, second, third, fourth, fifth).Compile();
        }

        internal static Func<TFirst, TSecond, TThird, TFourth, TFifth, TFirst> Map { get; private set; }
    }
}
