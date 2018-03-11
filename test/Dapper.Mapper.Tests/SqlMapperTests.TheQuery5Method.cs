// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using Xunit;

namespace Dapper.Mapper.Tests
{
    public abstract partial class SqlMapperTests
    {
        public class TheQuery5Method : SqlMapperTests
        {
            public TheQuery5Method()
                : base(columnNames: new[] { "FirstId", "SecondId", "ThirdId", "FourthId", "FifthId" })
            {
            }

            [Fact]
            public void ShouldPassCommandText()
            {
                // Arrange
                var commandText = "foo";

                // Act
                this.Connection.Query<First, Second, Third, Fourth, Fifth>(sql: commandText, param: null, transaction: this.Transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.Command)
                    .VerifySet(command => command.CommandText = commandText);
            }

            [Fact]
            public void ShouldPassParameters()
            {
                // Arrange
                var parameters = new { foo = 42 };

                // Act
                this.Connection.Query<First, Second, Third, Fourth, Fifth>(sql: "@foo", param: parameters, transaction: this.Transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.Parameter)
                    .VerifySet(parameter => parameter.ParameterName = "foo");
                Mock.Get(this.Parameter)
                    .VerifySet(parameter => parameter.Value = 42);
            }

            [Fact]
            public void ShouldPassTransaction()
            {
                // Act
                this.Connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.Transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.Command)
                    .VerifySet(command => command.Transaction = this.Transaction);
            }

            [Fact]
            public void ShouldPassBuffered()
            {
                // Act
                var result = this.Connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.Transaction, buffered: false, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.IsNotType<List<First>>(result);
            }

            [Fact]
            public void ShouldPassSplitOn()
            {
                // Act
                var result = this.Connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.Transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.Equal(1, result.First().Second.SecondId);
            }

            [Fact]
            public void ShouldPassCommandTimeout()
            {
                // Arrange
                var commandTimeout = 42;

                // Act
                this.Connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.Transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: commandTimeout, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.Command)
                    .VerifySet(command => command.CommandTimeout = commandTimeout);
            }

            [Fact]
            public void ShouldPassCommandType()
            {
                // Arrange
                var commandType = CommandType.TableDirect;

                // Act
                this.Connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.Transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: commandType);

                // Assert
                Mock.Get(this.Command)
                    .VerifySet(command => command.CommandType = commandType);
            }
        }
    }
}
