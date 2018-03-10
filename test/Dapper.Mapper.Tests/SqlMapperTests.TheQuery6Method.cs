﻿// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using Xunit;

namespace Dapper.Mapper.Tests
{
    public partial class SqlMapperTests
    {
        public class TheQuery6Method : SqlMapperTests
        {
            private readonly IDbConnection connection = Mock.Of<IDbConnection>();
            private readonly IDataParameterCollection parameters = Mock.Of<IDataParameterCollection>();
            private readonly IDbDataParameter parameter = Mock.Of<IDbDataParameter>();
            private readonly IDbCommand command = Mock.Of<IDbCommand>();
            private readonly IDataReader reader = Mock.Of<IDataReader>();
            private readonly IDbTransaction transaction = Mock.Of<IDbTransaction>();

            private readonly string[] columnNames = new[] { "FirstId", "SecondId", "ThirdId", "FourthId", "FifthId", "SixthId" };

            private int rowIndex;

            public TheQuery6Method()
            {
                Mock.Get(this.connection)
                    .Setup(connection => connection.CreateCommand())
                    .Returns(this.command);

                Mock.Get(this.command)
                    .Setup(command => command.ExecuteReader(It.IsAny<CommandBehavior>()))
                    .Returns(this.reader);

                Mock.Get(this.command)
                    .Setup(command => command.CreateParameter())
                    .Returns(this.parameter);

                Mock.Get(this.command)
                    .SetupGet(command => command.Parameters)
                    .Returns(this.parameters);

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

            [Fact]
            public void ShouldPassCommandText()
            {
                // Arrange
                var commandText = "foo";

                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth, Sixth>(sql: commandText, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId,SixthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.CommandText = commandText);
            }

            [Fact]
            public void ShouldPassParameters()
            {
                // Arrange
                var parameters = new { foo = 42 };

                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth, Sixth>(sql: "@foo", param: parameters, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId,SixthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.parameter)
                    .VerifySet(parameter => parameter.ParameterName = "foo");
                Mock.Get(this.parameter)
                    .VerifySet(parameter => parameter.Value = 42);
            }

            [Fact]
            public void ShouldPassTransaction()
            {
                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth, Sixth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId,SixthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.Transaction = this.transaction);
            }

            [Fact]
            public void ShouldPassBuffered()
            {
                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth, Sixth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: false, splitOn: "SecondId,ThirdId,FourthId,FifthId,SixthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.IsNotType<List<First>>(result);
            }

            [Fact]
            public void ShouldPassSplitOn()
            {
                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth, Sixth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId,SixthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.Equal(1, result.First().Second.SecondId);
            }

            [Fact]
            public void ShouldPassCommandTimeout()
            {
                // Arrange
                var commandTimeout = 42;

                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth, Sixth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId,SixthId", commandTimeout: commandTimeout, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.CommandTimeout = commandTimeout);
            }

            [Fact]
            public void ShouldPassCommandType()
            {
                // Arrange
                var commandType = CommandType.TableDirect;

                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth, Sixth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId,SixthId", commandTimeout: null, commandType: commandType);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.CommandType = commandType);
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
                public int ThridId { get; set; }

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
            }
        }
    }
}