using System;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.Rest.Repository;

namespace WorkWithDB.DAL.Rest
{
    public class RestUnitOfWork : IUnitOfWork
    {
        private IBlogPostRepository _blogPostRepository;
        private IAuthRepository _authRepository;


        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if(_blogPostRepository == null)
                    _blogPostRepository = new BlogPostRepository();
                return _blogPostRepository;
            }
        }

        public IBlogUserRepository BlogUserRepository { get; private set; }

        public IAuthRepository AuthRepository
        {
            get
            {
                if(_authRepository == null)
                    _authRepository = new AuthRepository();
                return _authRepository;
            }
        }

        /// <summary>
        /// Unsupported in REST version
        /// </summary>
        public void Commit()
        {
        }


        /// <summary>
        /// Unsupported in REST version
        /// </summary>
        public void RollBack()
        {
        }


        public void Dispose()
        {
        }
    }
}
