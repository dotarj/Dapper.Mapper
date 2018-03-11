// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Moq;
using Xunit;

namespace Dapper.Mapper.Tests
{
    public partial class SqlMapperTests
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
                this.connection.Query<First, Second, Third, Fourth, Fifth>(sql: commandText, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

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
                this.connection.Query<First, Second, Third, Fourth, Fifth>(sql: "@foo", param: parameters, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

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
                this.connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.Transaction = this.transaction);
            }

            [Fact]
            public void ShouldPassBuffered()
            {
                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: false, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.IsNotType<List<First>>(result);
            }

            [Fact]
            public void ShouldPassSplitOn()
            {
                // Act
                var result = this.connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.Equal(1, result.First().Second.SecondId);
            }

            [Fact]
            public void ShouldPassCommandTimeout()
            {
                // Arrange
                var commandTimeout = 42;

                // Act
                this.connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: commandTimeout, commandType: CommandType.Text);

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
                this.connection.Query<First, Second, Third, Fourth, Fifth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: "SecondId,ThirdId,FourthId,FifthId", commandTimeout: null, commandType: commandType);

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
            }
        }
    }
}
