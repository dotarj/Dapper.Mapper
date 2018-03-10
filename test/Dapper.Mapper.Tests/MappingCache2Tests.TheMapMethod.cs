// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using Xunit;

namespace Dapper.Mapper.Tests
{
    public class MappingCache2Tests
    {
        public class TheMapMethod
        {
            [Fact]
            public void ShouldSetProperties()
            {
                // Arrange
                var first = new First();
                var second = new Second();

                var map = MappingCache<First, Second>.Map;

                // Act
                var result = map(first, second);

                // Assert
                Assert.Equal(result.Second, second);
            }

            [Fact]
            public void ShouldNotSetPropertyIfFirstIsNull()
            {
                // Arrange
                First first = null;
                var second = new Second();

                var map = MappingCache<First, Second>.Map;

                // Act
                var result = map(first, second);

                // Assert
                Assert.Null(result);
            }

            private class First
            {
                public Second Second { get; set; }
            }

            private class Second
            {
            }
        }
    }
}