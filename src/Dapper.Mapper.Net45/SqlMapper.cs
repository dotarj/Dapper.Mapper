// Copyright (c) Arjen Post. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Mapper
{
    public static partial class SqlMapper
    {
        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Dapper.SqlMapper.QueryAsync<TFirst, TSecond, TFirst>(cnn, sql, MappingCache<TFirst, TSecond>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Dapper.SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Dapper.SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Dapper.SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Dapper.SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static async Task<IEnumerable<TFirst>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this IDbConnection cnn, string sql, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await Dapper.SqlMapper.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>(cnn, sql, MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.Map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }
    }
}
