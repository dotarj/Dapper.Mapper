// Copyright (c) Arjen Post. See LICENSE in the project root for license information.

using System.Data;
using System.Data.Common;

namespace Dapper.Mapper.Tests
{
    public abstract class DbConnectionMock : DbConnection
    {
        public abstract DbCommandMock CreateDbCommandMock();

        public abstract DbTransaction CreateDbTransactionMock();

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return this.CreateDbTransactionMock();
        }

        protected override DbCommand CreateDbCommand()
        {
            return this.CreateDbCommandMock();
        }
    }
}
