/*
 Copyright © 2013 Arjen Post (http://arjenpost.nl)
 License: http://www.apache.org/licenses/LICENSE-2.0 
 */

namespace Dapper.Mapper
{
    using System;
    using System.Linq.Expressions;

    internal static class MappingCache<TFirst, TSecond>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(TFirst), "first");
            var second = Expression.Parameter(typeof(TSecond), "second");

            var secondSetExpression = MappingCache.GetSetExpression(second, first);

            var blockExpression = Expression.Block(first, second, secondSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TFirst>>(blockExpression, first, second).Compile();
        }

        internal static Func<TFirst, TSecond, TFirst> Map { get; private set; }
    }
}
