// Copyright (c) Arjen Post. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Dapper.Mapper
{
    internal static class MappingCache
    {
        internal static Expression GetSetExpression(ParameterExpression sourceExpression, params ParameterExpression[] destinationExpressions)
        {
            var destination = destinationExpressions
                .Select(parameter => new
                {
                    Parameter = parameter,
                    Property = parameter.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(property => property.CanWrite && !property.GetIndexParameters().Any())
                        .Where(property => property.PropertyType == sourceExpression.Type || IsSubclassOf(sourceExpression.Type, property.PropertyType) || property.PropertyType.IsAssignableFrom(sourceExpression.Type))
                        .FirstOrDefault()
                })
                .Where(parameter => parameter.Property != null)
                .FirstOrDefault();

            if (destination == null)
            {
                throw new InvalidOperationException(string.Format("No writable property of type {0} found in types {1}.", sourceExpression.Type.FullName, string.Join(", ", destinationExpressions.Select(parameter => parameter.Type.FullName))));
            }
            
            return Expression.IfThen(
                Expression.Not(Expression.Equal(destination.Parameter, Expression.Constant(null))),
                Expression.Call(destination.Parameter, destination.Property.GetSetMethod(), sourceExpression));
        }

        private static bool IsSubclassOf(Type type, Type otherType)
        {
#if NETSTANDARD
            return type.GetTypeInfo().IsSubclassOf(otherType);
#else
            return type.IsSubclassOf(otherType);
#endif
        }
    }

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

    internal static class MappingCache<TFirst, TSecond, TThird, TFourth, TFifth>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(TFirst), "first");
            var second = Expression.Parameter(typeof(TSecond), "second");
            var third = Expression.Parameter(typeof(TThird), "third");
            var fourth = Expression.Parameter(typeof(TFourth), "fourth");
            var fifth = Expression.Parameter(typeof(TFifth), "fifth");

            var secondSetExpression = MappingCache.GetSetExpression(second, first);
            var thirdSetExpression = MappingCache.GetSetExpression(third, first, second);
            var fourthSetExpression = MappingCache.GetSetExpression(fourth, first, second, third);
            var fifthSetExpression = MappingCache.GetSetExpression(fifth, first, second, third, fourth);

            var blockExpression = Expression.Block(first, second, third, fourth, fifth, secondSetExpression, thirdSetExpression, fourthSetExpression, fifthSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>>(blockExpression, first, second, third, fourth, fifth).Compile();
        }

        internal static Func<TFirst, TSecond, TThird, TFourth, TFifth, TFirst> Map { get; private set; }
    }

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

    internal static class MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>
    {
        static MappingCache()
        {
            var first = Expression.Parameter(typeof(TFirst), "first");
            var second = Expression.Parameter(typeof(TSecond), "second");
            var third = Expression.Parameter(typeof(TThird), "third");
            var fourth = Expression.Parameter(typeof(TFourth), "fourth");
            var fifth = Expression.Parameter(typeof(TFifth), "fifth");
            var sixth = Expression.Parameter(typeof(TSixth), "sixth");
            var seventh = Expression.Parameter(typeof(TSeventh), "seventh");

            var secondSetExpression = MappingCache.GetSetExpression(second, first);
            var thirdSetExpression = MappingCache.GetSetExpression(third, first, second);
            var fourthSetExpression = MappingCache.GetSetExpression(fourth, first, second, third);
            var fifthSetExpression = MappingCache.GetSetExpression(fifth, first, second, third, fourth);
            var sixthSetExpression = MappingCache.GetSetExpression(sixth, first, second, third, fourth, fifth);
            var seventhSetExpression = MappingCache.GetSetExpression(seventh, first, second, third, fourth, fifth, sixth);

            var blockExpression = Expression.Block(first, second, third, fourth, fifth, sixth, seventh, secondSetExpression, thirdSetExpression, fourthSetExpression, fifthSetExpression, sixthSetExpression, seventhSetExpression, first);

            Map = Expression.Lambda<Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>>(blockExpression, first, second, third, fourth, fifth, sixth, seventh).Compile();
        }

        internal static Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst> Map { get; private set; }
    }
}
