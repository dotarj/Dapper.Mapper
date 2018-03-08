// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using Xunit;

namespace Dapper.Mapper.Tests
{
    public class MappingCache3Tests
    {
        [Fact]
        public void ShouldSetProperties()
        {
            // Arrange
            var first = new First();
            var second = new Second();
            var third = new Third();

            var map = MappingCache<First, Second, Third>.Map;

            // Act
            var result = map(first, second, third);

            // Assert
            Assert.Equal(result.Second, second);
            Assert.Equal(second.Third, third);
        }

        [Fact]
        public void ShouldNotSetPropertyIfFirstIsNull()
        {
            // Arrange
            First first = null;
            var second = new Second();
            var third = new Third();

            var map = MappingCache<First, Second, Third>.Map;

            // Act
            var result = map(first, second, third);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ShouldNotSetPropertyIfSecondIsNull()
        {
            // Arrange
            var first = new First();
            Second second = null;
            var third = new Third();

            var map = MappingCache<First, Second, Third>.Map;

            // Act
            var result = map(first, second, third);

            // Assert
            Assert.Null(result.Second);
        }

        private class First
        {
            public Second Second { get; set; }
        }

        private class Second
        {
            public Third Third { get; set; }
        }

        private class Third
        {
        }
    }
}