using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Options;
using WorkWithDB.DAL.Standard.Abstract;

namespace WorkWithDB.Standartd.DAL.SqlServer.Infrastructure
{
    public class SqlTransactionManager : ITransactionManager
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private IOptions<ConnectionStrings> _options;

        public SqlTransactionManager(SqlConnection connection,
            IOptions<ConnectionStrings> options)
        {

            _connection = connection;

            _connection = SqlConnectionFactory.CreateConnection(options.Value.DefaultConnection);
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
