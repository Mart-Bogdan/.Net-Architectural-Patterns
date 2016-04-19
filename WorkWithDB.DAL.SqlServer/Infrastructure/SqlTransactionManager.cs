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

        public void Dispose()
        {
            if (_transaction != null) 
                _transaction.Dispose();
        }

        public void Begin()
        {
            _transaction = _connection.BeginTransaction(/*IsolationLevel.ReadCommitted*/);
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