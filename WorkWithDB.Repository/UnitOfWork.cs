using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Abstract;

namespace WorkWithDB.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private string _connectionString;
        private SqlTransaction _transaction ;
        private SqlConnection _connection;
        private bool _disposableMethod;
        private IBlogUserRepository _blogUserRepository;
        private IBlogPostRepository _blogPostRepository;
        private IBlogCommentRepository _blogCommentRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rollBack">if value = true at dispose transaction will rollback else commited </param>
        public UnitOfWork (bool rollBack = false)
	    {
            _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            _disposableMethod = rollBack;
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        
        

        public IBlogCommentRepository BlogCommentRepository
        {
            get
            {
                if (_blogCommentRepository == null)
                    _blogCommentRepository = new BlogCommentRepository();
                return _blogCommentRepository;
            }
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
            if (!_disposableMethod)
            {
                _transaction.Commit();
            }
            else
            {
                _transaction.Rollback();
            }
            _connection.Close();
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
