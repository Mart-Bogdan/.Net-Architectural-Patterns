using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.SqlServer.Repository;

namespace WorkWithDB.DAL.SqlServer
{
    public class SqlServerAdoNetUnitOfWork : IUnitOfWork
    {
        private readonly SqlTransaction _transaction;
        private readonly SqlConnection _connection;

        private IBlogUserRepository _blogUserRepository;
        private IBlogPostRepository _blogPostRepository;
        /// <summary>
        /// 
        /// </summary>
        public SqlServerAdoNetUnitOfWork ()
	    {
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction(/*IsolationLevel.ReadCommitted*/);
        }

        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if (_blogPostRepository == null)
                    _blogPostRepository = new BlogPostRepository(_connection, _transaction);
                return _blogPostRepository;
            }
        }

        public IBlogUserRepository BlogUserRepository
        {
            get
            {
                if (_blogUserRepository == null)
                    _blogUserRepository = new BlogUserRepository(_connection, _transaction);
                return _blogUserRepository;
            }
        }

        public void Dispose()
        {
            try
            {
                if (_transaction != null) _transaction.Dispose();
            }
            finally
            {
                _connection.Dispose();                
            }
        }


        public void Commit()
        {
            if (_transaction != null) _transaction.Commit();
        }

        public void RollBack()
        {
            if (_transaction != null) _transaction.Rollback();
        }
    }
}
