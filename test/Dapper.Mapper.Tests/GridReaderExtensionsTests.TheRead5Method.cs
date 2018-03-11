// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dapper.Mapper.Tests
{
    public abstract partial class GridReaderExtensionsTests : BaseTests
    {
        public class TheRead5Method : GridReaderExtensionsTests
        {
            public TheRead5Method()
                : base(columnNames: new[] { "FirstId", "SecondId", "ThirdId", "FourthId", "FifthId" })
            {
            }

            [Fact]
            public void ShouldPassSplitOn()
            {
                // Arrange
                var gridReader = this.Connection.QueryMultiple(string.Empty);

                // Act
                var result = gridReader.Read<First, Second, Third, Fourth, Fifth>(splitOn: string.Join(",", this.ColumnNames), buffered: true);

                // Assert
                Assert.Equal(1, result.First().Second.Third.Fourth.Fifth.FifthId);
            }

            [Fact]
            public void ShouldPassBuffered()
            {
                // Arrange
                var gridReader = this.Connection.QueryMultiple(string.Empty);

                // Act
                var result = gridReader.Read<First, Second, Third, Fourth, Fifth>(splitOn: string.Join(",", this.ColumnNames), buffered: false);

                // Assert
                Assert.IsNotType<List<First>>(result);
            }
        }
    }
}
