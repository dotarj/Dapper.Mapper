// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dapper.Mapper.Tests
{
    public partial class GridReaderExtensionsTests : BaseTests
    {
        public class TheRead7Method : GridReaderExtensionsTests
        {
            public TheRead7Method()
                : base(columnNames: new[] { "FirstId", "SecondId", "ThirdId", "FourthId", "FifthId", "SixthId", "SeventhId" })
            {
            }

            [Fact]
            public void ShouldPassSplitOn()
            {
                // Arrange
                var gridReader = this.Connection.QueryMultiple(string.Empty);

                // Act
                var result = gridReader.Read<First, Second, Third, Fourth, Fifth, Sixth, Seventh>(splitOn: string.Join(",", this.ColumnNames), buffered: true);

                // Assert
                Assert.Equal(1, result.First().Second.Third.Fourth.Fifth.Sixth.Seventh.SeventhId);
            }

            [Fact]
            public void ShouldPassBuffered()
            {
                // Arrange
                var gridReader = this.Connection.QueryMultiple(string.Empty);

                // Act
                var result = gridReader.Read<First, Second, Third, Fourth, Fifth, Sixth, Seventh>(splitOn: string.Join(",", this.ColumnNames), buffered: false);

                // Assert
                Assert.IsNotType<List<First>>(result);
            }
        }
    }
}
