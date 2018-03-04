// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

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
}
