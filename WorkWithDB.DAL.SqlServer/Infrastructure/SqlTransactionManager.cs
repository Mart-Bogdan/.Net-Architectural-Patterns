using System;
using System.Data.SqlClient;
using WorkWithDB.DAL.Abstract;

namespace WorkWithDB.DAL.SqlServer.Infrastructure
{
    internal class SqlTransactionManager : ITransactionManager
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public SqlTransactionManager(SqlConnection connection)
        {
            _connection = connection;
        }

        internal SqlTransaction CurrentTransaction
        {
            get { return _transaction; }
        }

        ~SqlTransactionManager()
        {
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }

        public IDisposable Begin()
        {
            if (_transaction != null)
            {
                //TODO add nested TX, aka. checkpoints support
                _transaction.Dispose();
            }
            _transaction = _connection.BeginTransaction(/*IsolationLevel.ReadCommitted*/);
            return this;
        }

        public void Commit()
        {
            if (_transaction != null) _transaction.Commit();
            _transaction = null;
        }

        public void RollBack()
        {
            if (_transaction != null) _transaction.Rollback();
            _transaction = null;
        }

    }
}