/*
 Copyright © 2013 Arjen Post (http://arjenpost.nl)
 License: http://www.apache.org/licenses/LICENSE-2.0 
 */

namespace Dapper.Mapper
{
    using System.Collections.Generic;
    using System.Data;

    public static class SqlMapper
    {
        public static IEnumerable<TFirst> Query<TFirst, TSecond>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return Dapper.SqlMapper.Query<TFirst, TSecond, TFirst>(cnn, sql, MappingCache<TFirst, TSecond>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth, TFifth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird, TFourth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static IEnumerable<TFirst> Query<TFirst, TSecond, TThird>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return Dapper.SqlMapper.Query<TFirst, TSecond, TThird, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }
    }
}
