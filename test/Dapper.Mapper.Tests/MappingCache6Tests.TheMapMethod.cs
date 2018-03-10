// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using Xunit;

namespace Dapper.Mapper.Tests
{
    public class MappingCache6Tests
    {
        public class TheMapMethod
        {
            [Fact]
            public void ShouldSetProperties()
            {
                // Arrange
                var first = new First();
                var second = new Second();
                var third = new Third();
                var fourth = new Fourth();
                var fifth = new Fifth();
                var sixth = new Sixth();

                var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth>.Map;

                // Act
                var result = map(first, second, third, fourth, fifth, sixth);

                // Assert
                Assert.Equal(result.Second, second);
                Assert.Equal(second.Third, third);
                Assert.Equal(third.Fourth, fourth);
            }

            [Fact]
            public void ShouldNotSetPropertyIfFirstIsNull()
            {
                // Arrange
                First first = null;
                var second = new Second();
                var third = new Third();
                var fourth = new Fourth();
                var fifth = new Fifth();
                var sixth = new Sixth();

                var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth>.Map;

                // Act
                var result = map(first, second, third, fourth, fifth, sixth);

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
                var fourth = new Fourth();
                var fifth = new Fifth();
                var sixth = new Sixth();

                var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth>.Map;

                // Act
                var result = map(first, second, third, fourth, fifth, sixth);

                // Assert
                Assert.Null(result.Second);
            }

            [Fact]
            public void ShouldNotSetPropertyIfThirdIsNull()
            {
                // Arrange
                var first = new First();
                var second = new Second();
                Third third = null;
                var fourth = new Fourth();
                var fifth = new Fifth();
                var sixth = new Sixth();

                var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth>.Map;

                // Act
                var result = map(first, second, third, fourth, fifth, sixth);

                // Assert
                Assert.Null(second.Third);
            }

            [Fact]
            public void ShouldNotSetPropertyIfFourthIsNull()
            {
                // Arrange
                var first = new First();
                var second = new Second();
                var third = new Third();
                Fourth fourth = null;
                var fifth = new Fifth();
                var sixth = new Sixth();

                var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth>.Map;

                // Act
                var result = map(first, second, third, fourth, fifth, sixth);

                // Assert
                Assert.Null(third.Fourth);
            }

            [Fact]
            public void ShouldNotSetPropertyIfFifthIsNull()
            {
                // Arrange
                var first = new First();
                var second = new Second();
                var third = new Third();
                var fourth = new Fourth();
                Fifth fifth = null;
                var sixth = new Sixth();

                var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth>.Map;

                // Act
                var result = map(first, second, third, fourth, fifth, sixth);

                // Assert
                Assert.Null(fourth.Fifth);
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
                public Fourth Fourth { get; set; }
            }

            private class Fourth
            {
                public Fifth Fifth { get; set; }
            }

            private class Fifth
            {
                public Sixth Sixth { get; set; }
            }

            private class Sixth
            {
            }
        }
    }
}