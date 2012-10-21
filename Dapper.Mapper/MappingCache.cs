/*
 Copyright © 2012 Arjen Post (http://arjenpost.nl)
 License: http://www.apache.org/licenses/LICENSE-2.0 
 */

namespace Dapper.Mapper
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class MappingCache
    {
        internal static Expression GetSetExpression(ParameterExpression sourceExpression, params ParameterExpression[] destinationExpressions)
        {
            var destination = destinationExpressions
                .Select(parameter => new
                {
                    Parameter = parameter,
                    Property = parameter.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(property => property.CanWrite)
                        .Where(property => property.PropertyType == sourceExpression.Type || sourceExpression.Type.IsSubclassOf(property.PropertyType))
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
    }
}
