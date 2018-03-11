// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Mapper.Tests
{
    public abstract class DbCommandMock : DbCommand
    {
        public abstract DbParameterCollection DbParameterCollectionMock { get; }

        protected override DbParameterCollection DbParameterCollection => this.DbParameterCollectionMock;

        public abstract DbParameter CreateDbParameterMock();

        public abstract DbDataReader ExecuteDbDataReaderMock(CommandBehavior behavior);

        protected override DbParameter CreateDbParameter()
        {
            return this.CreateDbParameterMock();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return this.ExecuteDbDataReaderMock(behavior);
        }

        protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            return Task.FromResult(this.ExecuteDbDataReaderMock(behavior));
        }
    }
}
