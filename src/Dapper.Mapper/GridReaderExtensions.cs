// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Dapper.Mapper
{
    public static class GridReaderExtensions
    {
        public static IEnumerable<TFirst> Read<TFirst, TSecond>(this Dapper.SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
        {
            return gridReader.Read(MappingCache<TFirst, TSecond>.Map, splitOn, buffered);
        }

        public static IEnumerable<TFirst> Read<TFirst, TSecond, TThird>(this Dapper.SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
        {
            return gridReader.Read(MappingCache<TFirst, TSecond, TThird>.Map, splitOn, buffered);
        }

        public static IEnumerable<TFirst> Read<TFirst, TSecond, TThird, TFourth>(this Dapper.SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
        {
            return gridReader.Read(MappingCache<TFirst, TSecond, TThird, TFourth>.Map, splitOn, buffered);
        }

        public static IEnumerable<TFirst> Read<TFirst, TSecond, TThird, TFourth, TFifth>(this Dapper.SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
        {
            return gridReader.Read(MappingCache<TFirst, TSecond, TThird, TFourth, TFifth>.Map, splitOn, buffered);
        }

        public static IEnumerable<TFirst> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(this Dapper.SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
        {
            return gridReader.Read(MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>.Map, splitOn, buffered);
        }

        public static IEnumerable<TFirst> Read<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this Dapper.SqlMapper.GridReader gridReader, string splitOn = "id", bool buffered = true)
        {
            return gridReader.Read(MappingCache<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>.Map, splitOn, buffered);
        }
    }
}
