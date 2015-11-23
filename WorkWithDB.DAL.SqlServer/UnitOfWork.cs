using System.Configuration;
using System.Data.SqlClient;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.SqlServer.Repository;

namespace WorkWithDB.DAL.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private string _connectionString;
        private SqlTransaction _transaction ;
        private SqlConnection _connection;

        private IBlogUserRepository _blogUserRepository;
        private IBlogPostRepository _blogPostRepository;
        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork ()
	    {
            _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if (_blogPostRepository == null)
                    _blogPostRepository = new BlogPostRepository(_connection);
                return _blogPostRepository;
            }
        }

        public IBlogUserRepository BlogUserRepository
        {
            get
            {
                if (_blogUserRepository == null)
                    _blogUserRepository = new BlogUserRepository(_connection);
                return _blogUserRepository;
            }
        }

        public void Dispose()
        {
            try
            {
                _transaction.Dispose();
            }
            finally
            {
                _connection.Dispose();                
            }
        }


        public void Commit()
        {
            _transaction.Commit();
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }
    }
}
