// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Data;
using System.Data.Common;
using Moq;

namespace Dapper.Mapper.Tests
{
    public abstract class BaseTests
    {
        private int rowIndex;

        public BaseTests(string[] columnNames)
        {
            this.ColumnNames = columnNames;

            this.SetupConnection();
            this.SetupCommand();
            this.SetupReader();
        }

        protected DbConnectionMock Connection { get; } = Mock.Of<DbConnectionMock>();

        protected DbCommandMock Command { get; } = Mock.Of<DbCommandMock>();

        protected DbParameterCollection Parameters { get; } = Mock.Of<DbParameterCollection>();

        protected DbParameter Parameter { get; } = Mock.Of<DbParameter>();

        protected DbDataReader Reader { get; } = Mock.Of<DbDataReader>();

        protected DbTransaction Transaction { get; } = Mock.Of<DbTransaction>();

        protected string[] ColumnNames { get; }

        private void SetupConnection()
        {
            Mock.Get(this.Connection).CallBase = true;

            Mock.Get(this.Connection)
                .Setup(connection => connection.CreateDbCommandMock())
                .Returns(this.Command);
        }

        private void SetupCommand()
        {
            Mock.Get(this.Command).CallBase = true;

            Mock.Get(this.Command)
                .SetupGet(command => command.DbParameterCollectionMock)
                .Returns(this.Parameters);

            Mock.Get(this.Command)
                .Setup(command => command.ExecuteDbDataReaderMock(It.IsAny<CommandBehavior>()))
                .Returns(this.Reader);

            Mock.Get(this.Command)
                .Setup(command => command.CreateDbParameterMock())
                .Returns(this.Parameter);
        }

        private void SetupReader()
        {
            Mock.Get(this.Reader)
                .SetupGet(reader => reader.FieldCount)
                .Returns(this.ColumnNames.Length);

            Mock.Get(this.Reader)
                .Setup(reader => reader.GetName(It.IsAny<int>()))
                .Returns<int>(i => this.ColumnNames[i]);

            Mock.Get(this.Reader)
                .Setup(reader => reader.GetFieldType(It.IsAny<int>()))
                .Returns(typeof(int));

            Mock.Get(this.Reader)
                .Setup(reader => reader.Read())
                .Returns(() => (this.rowIndex++ < 1));

            Mock.Get(this.Reader)
                .SetupGet(reader => reader[It.IsAny<int>()])
                .Returns(1);
        }

        public class First
        {
            public int FirstId { get; set; }

            public Second Second { get; set; }
        }

        public class Second
        {
            public int SecondId { get; set; }

            public Third Third { get; set; }
        }

        public class Third
        {
            public int ThirdId { get; set; }

            public Fourth Fourth { get; set; }
        }

        public class Fourth
        {
            public int FourthId { get; set; }

            public Fifth Fifth { get; set; }
        }

        public class Fifth
        {
            public int FifthId { get; set; }

            public Sixth Sixth { get; set; }
        }

        public class Sixth
        {
            public int SixthId { get; set; }

            public Seventh Seventh { get; set; }
        }

        public class Seventh
        {
            public int SeventhId { get; set; }
        }
    }
}
