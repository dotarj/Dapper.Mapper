// Copyright (c) Arjen Post. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dapper.Mapper.Tests
{
    [TestClass]
    public class MappingCacheTests
    {
        [TestMethod]
        public void MappingCache2()
        {
            // Arrange
            var first = new First();
            var second = new Second();

            var map = MappingCache<First, Second>.Map;

            // Act
            var result = map(first, second);

            // Assert
            Assert.AreEqual(result.Second, second);
        }

        [TestMethod]
        public void MappingCache2_2()
        {
            // Arrange
            var first = new First();
            Second second = null;

            var map = MappingCache<First, Second>.Map;

            // Act
            var result = map(first, second);

            // Assert
            Assert.IsNull(result.Second);
        }

        [TestMethod]
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
            Assert.AreEqual(result.Second.Third, third);
        }

        [TestMethod]
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
            Assert.IsNull(result.Second);
        }

        [TestMethod]
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
            Assert.AreEqual(result.Second.Third.Fourth, fourth);
        }

        [TestMethod]
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
            Assert.IsNull(result.Second);
        }

        [TestMethod]
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
            Assert.AreEqual(result.Second.Third.Fourth.Fifth, fifth);
        }

        [TestMethod]
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
            Assert.IsNull(result.Second);
        }

        [TestMethod]
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
            Assert.AreEqual(result.Second.Third.Fourth.Fifth.Sixth, sixth);
        }

        [TestMethod]
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
            Assert.IsNull(result.Second);
        }

        [TestMethod]
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
            Assert.AreEqual(result.Second.Third.Fourth.Fifth.Sixth.Seventh, seventh);
        }

        [TestMethod]
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
            Assert.IsNull(result.Second);
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
}
