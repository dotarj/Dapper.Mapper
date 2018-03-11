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
        public class TheQueryAsync4Method : SqlMapperTests
        {
            public TheQueryAsync4Method()
                : base(columnNames: new[] { "FirstId", "SecondId", "ThirdId", "FourthId" })
            {
            }

            [Fact]
            public async Task ShouldPassCommandText()
            {
                // Arrange
                var commandText = "foo";

                // Act
                await this.connection.QueryAsync<First, Second, Third, Fourth>(sql: commandText, param: null, transaction: this.transaction, buffered: true, splitOn: string.Join(",", this.columnNames), commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.CommandText = commandText);
            }

            [Fact]
            public async Task ShouldPassParameters()
            {
                // Arrange
                var parameters = new { foo = 42 };

                // Act
                await this.connection.QueryAsync<First, Second, Third, Fourth>(sql: "@foo", param: parameters, transaction: this.transaction, buffered: true, splitOn: string.Join(",", this.columnNames), commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.parameter)
                    .VerifySet(parameter => parameter.ParameterName = "foo");
                Mock.Get(this.parameter)
                    .VerifySet(parameter => parameter.Value = 42);
            }

            [Fact]
            public async Task ShouldPassTransaction()
            {
                // Act
                await this.connection.QueryAsync<First, Second, Third, Fourth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: string.Join(",", this.columnNames), commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.Transaction = this.transaction);
            }

            [Fact]
            public async Task ShouldPassBuffered()
            {
                // Act
                var result = await this.connection.QueryAsync<First, Second, Third, Fourth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: false, splitOn: string.Join(",", this.columnNames), commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.IsNotType<List<First>>(result);
            }

            [Fact]
            public async Task ShouldPassSplitOn()
            {
                // Act
                var result = await this.connection.QueryAsync<First, Second, Third, Fourth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: string.Join(",", this.columnNames), commandTimeout: null, commandType: CommandType.Text);

                // Assert
                Assert.Equal(1, result.First().Second.Third.Fourth.FourthId);
            }

            [Fact]
            public async Task ShouldPassCommandTimeout()
            {
                // Arrange
                var commandTimeout = 42;

                // Act
                await this.connection.QueryAsync<First, Second, Third, Fourth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: string.Join(",", this.columnNames), commandTimeout: commandTimeout, commandType: CommandType.Text);

                // Assert
                Mock.Get(this.command)
                    .VerifySet(command => command.CommandTimeout = commandTimeout);
            }

            [Fact]
            public async Task ShouldPassCommandType()
            {
                // Arrange
                var commandType = CommandType.TableDirect;

                // Act
                await this.connection.QueryAsync<First, Second, Third, Fourth>(sql: string.Empty, param: null, transaction: this.transaction, buffered: true, splitOn: string.Join(",", this.columnNames), commandTimeout: null, commandType: commandType);

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
                public int ThirdId { get; set; }

                public Fourth Fourth { get; set; }
            }

            public class Fourth
            {
                public int FourthId { get; set; }
            }
        }
    }
}
