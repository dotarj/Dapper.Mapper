// Copyright (c) Arjen Post. See License.txt in the project root for license information.

using Xunit;

namespace Dapper.Mapper.Tests
{
    public class MappingCacheTests
    {
        [Fact]
        public void MappingCache2()
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
        public void MappingCache2_2()
        {
            // Arrange
            var first = new First();
            Second second = null;

            var map = MappingCache<First, Second>.Map;

            // Act
            var result = map(first, second);

            // Assert
            Assert.Null(result.Second);
        }

        [Fact]
        public void MappingCache3()
        {
            // Arrange
            var first = new First();
            var second = new Second();
            var third = new Third();

            var map = MappingCache<First, Second, Third>.Map;

            // Act
            var result = map(first, second, third);

            // Assert
            Assert.Equal(result.Second.Third, third);
        }

        [Fact]
        public void MappingCache3_2()
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

        [Fact]
        public void MappingCache4()
        {
            // Arrange
            var first = new First();
            var second = new Second();
            var third = new Third();
            var fourth = new Fourth();

            var map = MappingCache<First, Second, Third, Fourth>.Map;

            // Act
            var result = map(first, second, third, fourth);

            // Assert
            Assert.Equal(result.Second.Third.Fourth, fourth);
        }

        [Fact]
        public void MappingCache4_2()
        {
            // Arrange
            var first = new First();
            Second second = null;
            var third = new Third();
            var fourth = new Fourth();

            var map = MappingCache<First, Second, Third, Fourth>.Map;

            // Act
            var result = map(first, second, third, fourth);

            // Assert
            Assert.Null(result.Second);
        }

        [Fact]
        public void MappingCache5()
        {
            // Arrange
            var first = new First();
            var second = new Second();
            var third = new Third();
            var fourth = new Fourth();
            var fifth = new Fifth();

            var map = MappingCache<First, Second, Third, Fourth, Fifth>.Map;

            // Act
            var result = map(first, second, third, fourth, fifth);

            // Assert
            Assert.Equal(result.Second.Third.Fourth.Fifth, fifth);
        }

        [Fact]
        public void MappingCache5_2()
        {
            // Arrange
            var first = new First();
            Second second = null;
            var third = new Third();
            var fourth = new Fourth();
            var fifth = new Fifth();

            var map = MappingCache<First, Second, Third, Fourth, Fifth>.Map;

            // Act
            var result = map(first, second, third, fourth, fifth);

            // Assert
            Assert.Null(result.Second);
        }

        [Fact]
        public void MappingCache6()
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
            Assert.Equal(result.Second.Third.Fourth.Fifth.Sixth, sixth);
        }

        [Fact]
        public void MappingCache6_2()
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
        public void MappingCache7()
        {
            // Arrange
            var first = new First();
            var second = new Second();
            var third = new Third();
            var fourth = new Fourth();
            var fifth = new Fifth();
            var sixth = new Sixth();
            var seventh = new Seventh();

            var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth, Seventh>.Map;

            // Act
            var result = map(first, second, third, fourth, fifth, sixth, seventh);

            // Assert
            Assert.Equal(result.Second.Third.Fourth.Fifth.Sixth.Seventh, seventh);
        }

        [Fact]
        public void MappingCache7_2()
        {
            // Arrange
            var first = new First();
            Second second = null;
            var third = new Third();
            var fourth = new Fourth();
            var fifth = new Fifth();
            var sixth = new Sixth();
            var seventh = new Seventh();

            var map = MappingCache<First, Second, Third, Fourth, Fifth, Sixth, Seventh>.Map;

            // Act
            var result = map(first, second, third, fourth, fifth, sixth, seventh);

            // Assert
            Assert.Null(result.Second);
        }

        [Fact]
        public void MappingCache8()
        {
            // Arrange
            var withInterface = new Eighth();
            var test = new Test();

            var map = MappingCache<Eighth, Test>.Map;

            // Act
            var result = map(withInterface, test);

            // Assert
            Assert.NotNull(result.Test);
        }
    }

    public class First
    {
        public Second Second { get; set; }
    }

    public class Second
    {
        public ThirdBase Third { get; set; }
    }

    public class ThirdBase
    {
        public Fourth Fourth { get; set; }
    }

    public class Third : ThirdBase
    {
    }

    public class Fourth
    {
        public Fifth Fifth { get; set; }
    }

    public class Fifth
    {
        public Sixth Sixth { get; set; }
    }

    public class Sixth
    {
        public Seventh Seventh { get; set; }
    }

    public class Seventh
    {
    }

    public class Eighth
    {
        public ITest Test { get; set; }
    }

    public interface ITest { }

    public class Test : ITest { }
}