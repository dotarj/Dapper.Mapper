// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper.Mapper;
using Moq;
using Xunit;

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
    }
}
