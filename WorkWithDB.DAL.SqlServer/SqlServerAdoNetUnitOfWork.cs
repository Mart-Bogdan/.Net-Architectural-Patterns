using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.SqlServer.Infrastructure;
using WorkWithDB.DAL.SqlServer.Repository;

namespace WorkWithDB.DAL.SqlServer
{
    [Obsolete("This class is left for use by non DI aware clients, like our UI application")]
    public class SqlServerAdoNetUnitOfWork : IUnitOfWork
    {
        private readonly SqlConnection _connection;

        private IBlogUserRepository _blogUserRepository;
        private IBlogPostRepository _blogPostRepository;
        private IAuthRepository _authRepository;
        private SqlTransactionManager _transactionManager;

        /// <summary>
        /// 
        /// </summary>
        public SqlServerAdoNetUnitOfWork()
        {
            _connection = SqlConnectionFactory.CreateConnection();
            _connection.Open();
        }

        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if (_blogPostRepository == null)
                    _blogPostRepository = new BlogPostRepository(_connection, TransactionManager);
                return _blogPostRepository;
            }
        }

        public IBlogUserRepository BlogUserRepository
        {
            get
            {
                if (_blogUserRepository == null)
                    _blogUserRepository = new BlogUserRepository(_connection, TransactionManager);
                return _blogUserRepository;
            }
        }

        public IAuthRepository AuthRepository
        {
            get
            {
                if (_authRepository == null)
                    _authRepository = new AuthRepository(BlogUserRepository);
                return _authRepository;
            }
        }
        ITransactionManager IUnitOfWork.TransactionManager
        {
            get { return TransactionManager; }
        }

        internal SqlTransactionManager TransactionManager
        {
            get
            {
                if (_transactionManager == null)
                    _transactionManager = new SqlTransactionManager(_connection);
                return _transactionManager;
            }
        }

        public void Dispose()
        {
            if (_transactionManager != null)
                _transactionManager.Dispose();

            _connection.Dispose();
        }
    }
}
