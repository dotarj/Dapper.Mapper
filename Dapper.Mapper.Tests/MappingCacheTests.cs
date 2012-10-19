namespace Dapper.Mapper.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
