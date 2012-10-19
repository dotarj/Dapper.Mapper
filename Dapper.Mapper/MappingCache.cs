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
        internal static Tuple<ParameterExpression, PropertyInfo> GetParameterAndProperty(Type propertyType, params ParameterExpression[] parameters)
        {
            var result = parameters
                .Select(parameter => new Tuple<ParameterExpression, PropertyInfo>(parameter, parameter.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(property => property.CanWrite)
                    .Where(property => property.PropertyType == propertyType || propertyType.IsSubclassOf(property.PropertyType))
                    .FirstOrDefault()))
                .Where(tuple => tuple.Item2 != null)
                .FirstOrDefault();

            if (result == null)
            {
                throw new InvalidOperationException(string.Format("No writable property of type {0} found in types {1}.", propertyType.FullName, string.Join(", ", parameters.Select(parameter => parameter.Type.FullName))));
            }

            return result;
        }
    }
}
