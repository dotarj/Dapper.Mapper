// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Data;
using System.Data.Common;
using Moq;

namespace Dapper.Mapper.Tests
{
    public partial class SqlMapperTests
    {
        private readonly DbConnectionMock connection = Mock.Of<DbConnectionMock>();
        private readonly DbCommandMock command = Mock.Of<DbCommandMock>();
        private readonly DbParameterCollection parameters = Mock.Of<DbParameterCollection>();
        private readonly DbParameter parameter = Mock.Of<DbParameter>();
        private readonly DbDataReader reader = Mock.Of<DbDataReader>();
        private readonly DbTransaction transaction = Mock.Of<DbTransaction>();

        private readonly string[] columnNames;

        private int rowIndex;

        public SqlMapperTests(string[] columnNames)
        {
            this.columnNames = columnNames;

            this.SetupConnection();
            this.SetupCommand();
            this.SetupReader();
        }

        private void SetupConnection()
        {
            Mock.Get(this.connection).CallBase = true;

            Mock.Get(this.connection)
                .Setup(connection => connection.CreateDbCommandMock())
                .Returns(this.command);
        }

        private void SetupCommand()
        {
            Mock.Get(this.command).CallBase = true;

            Mock.Get(this.command)
                .SetupGet(command => command.DbParameterCollectionMock)
                .Returns(this.parameters);

            Mock.Get(this.command)
                .Setup(command => command.ExecuteDbDataReaderMock(It.IsAny<CommandBehavior>()))
                .Returns(this.reader);

            Mock.Get(this.command)
                .Setup(command => command.CreateDbParameterMock())
                .Returns(this.parameter);
        }

        private void SetupReader()
        {
            Mock.Get(this.reader)
                .SetupGet(reader => reader.FieldCount)
                .Returns(this.columnNames.Length);

            Mock.Get(this.reader)
                .Setup(reader => reader.GetName(It.IsAny<int>()))
                .Returns<int>(i => this.columnNames[i]);

            Mock.Get(this.reader)
                .Setup(reader => reader.GetFieldType(It.IsAny<int>()))
                .Returns(typeof(int));

            Mock.Get(this.reader)
                .Setup(reader => reader.Read())
                .Returns(() => (this.rowIndex++ < 1));

            Mock.Get(this.reader)
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
