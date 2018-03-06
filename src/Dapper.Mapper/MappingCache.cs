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
                        .FirstOrDefault(property => IsWritable(property) && IsOfType(property, sourceExpression.Type))
                })
                .FirstOrDefault(parameter => parameter.Property != null);

            if (destination == null)
            {
                throw new InvalidOperationException(string.Format("No writable property of type {0} found in types {1}.", sourceExpression.Type.FullName, string.Join(", ", destinationExpressions.Select(parameter => parameter.Type.FullName))));
            }

            return Expression.IfThen(
                Expression.Not(Expression.Equal(destination.Parameter, Expression.Constant(null))),
                Expression.Call(destination.Parameter, destination.Property.GetSetMethod(), sourceExpression));
        }

        private static bool IsWritable(PropertyInfo property)
        {
            return property.CanWrite && !property.GetIndexParameters().Any();
        }

        private static bool IsOfType(PropertyInfo property, Type type)
        {
            return property.PropertyType == type || IsSubclassOf(type, property.PropertyType) || property.PropertyType.IsAssignableFrom(type);
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
