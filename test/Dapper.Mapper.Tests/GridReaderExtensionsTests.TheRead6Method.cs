// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dapper.Mapper.Tests
{
    public abstract partial class GridReaderExtensionsTests : BaseTests
    {
        public class TheRead6Method : GridReaderExtensionsTests
        {
            public TheRead6Method()
                : base(columnNames: new[] { "FirstId", "SecondId", "ThirdId", "FourthId", "FifthId", "SixthId" })
            {
            }

            [Fact]
            public void ShouldPassSplitOn()
            {
                // Arrange
                var gridReader = this.Connection.QueryMultiple(string.Empty);

                // Act
                var result = gridReader.Read<First, Second, Third, Fourth, Fifth, Sixth>(splitOn: string.Join(",", this.ColumnNames), buffered: true);

                // Assert
                Assert.Equal(1, result.First().Second.Third.Fourth.Fifth.Sixth.SixthId);
            }

            [Fact]
            public void ShouldPassBuffered()
            {
                // Arrange
                var gridReader = this.Connection.QueryMultiple(string.Empty);

                // Act
                var result = gridReader.Read<First, Second, Third, Fourth, Fifth, Sixth>(splitOn: string.Join(",", this.ColumnNames), buffered: false);

                // Assert
                Assert.IsNotType<List<First>>(result);
            }
        }
    }
}
