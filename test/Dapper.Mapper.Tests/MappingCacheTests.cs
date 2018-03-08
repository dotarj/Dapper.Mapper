// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System;
using System.Linq.Expressions;
using Xunit;

namespace Dapper.Mapper.Tests
{
    public class MappingCacheTests
    {
        private interface IInterfaceSecond
        {
        }

        [Fact]
        public void ShouldThrowIfNoWritablePropertyFound()
        {
            // Arrange
            var first = Expression.Parameter(typeof(NoWritableFirst), "first");
            var second = Expression.Parameter(typeof(NoWritableSecond), "second");

            // Assert
            Assert.Throws<InvalidOperationException>(() => MappingCache.GetSetExpression(second, first));
        }

        [Fact]
        public void ShouldSetPropertiesWithCorrectBaseType()
        {
            // Arrange
            var first = new BaseTypeFirst();
            var second = new BaseTypeSecond();

            var map = MappingCache<BaseTypeFirst, BaseTypeSecond>.Map;

            // Act
            var result = map(first, second);

            // Assert
            Assert.Equal(result.Second, second);
        }

        [Fact]
        public void ShouldSetPropertiesWithCorrectInterface()
        {
            // Arrange
            var first = new InterfaceFirst();
            var second = new InterfaceSecond();

            var map = MappingCache<InterfaceFirst, InterfaceSecond>.Map;

            // Act
            var result = map(first, second);

            // Assert
            Assert.Equal(result.Second, second);
        }

        [Fact]
        public void ShouldNotSetNonWritableProperties()
        {
            // Arrange
            var first = new NonWriteableFirst();
            var second = new NonWriteableSecond();
            var third = new NonWriteableThird();

            var map = MappingCache<NonWriteableFirst, NonWriteableThird, NonWriteableSecond>.Map;

            // Act
            var result = map(first, third, second);

            // Assert
            Assert.Null(result.Second);
        }

        [Fact]
        public void ShouldNotSetIndexerProperties()
        {
            // Arrange
            var first = new IndexerFirst();
            var third = new IndexerThird();
            var second = new IndexerSecond();

            var map = MappingCache<IndexerFirst, IndexerThird, IndexerSecond>.Map;

            // Act
            var result = map(first, third, second);

            // Assert
            Assert.Null(result[0]);
        }

        private class BaseTypeFirst
        {
            public BaseTypeSecondBase Second { get; set; }
        }

        private class BaseTypeSecondBase
        {
        }

        private class BaseTypeSecond : BaseTypeSecondBase
        {
        }

        private class InterfaceFirst
        {
            public IInterfaceSecond Second { get; set; }
        }

        private class InterfaceSecond : IInterfaceSecond
        {
        }

        private class NonWriteableFirst
        {
            public NonWriteableSecond Second { get; }

            public NonWriteableThird Third { get; set; }
        }

        private class NonWriteableSecond
        {
        }

        private class NonWriteableThird
        {
            public NonWriteableSecond Second { get; set; }
        }

        private class IndexerFirst
        {
            private IndexerSecond indexerSecond;

            public IndexerThird IndexerThird { get; set; }

            public IndexerSecond this[int index]
            {
                get { return this.indexerSecond; }
                set { this.indexerSecond = value; }
            }
        }

        private class IndexerSecond
        {
        }

        private class IndexerThird
        {
            public IndexerSecond IndexerSecond { get; set; }
        }

        private class NoWritableFirst
        {
            public NoWritableSecond Second { get; }
        }

        private class NoWritableSecond
        {
        }
    }
}