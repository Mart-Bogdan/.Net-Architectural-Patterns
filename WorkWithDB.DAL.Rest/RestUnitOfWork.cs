using System;
using WorkWithDB.DAL.Abstract.Rest;
using WorkWithDB.DAL.Rest.Repository;

namespace WorkWithDB.DAL.Rest
{
    public class RestUnitOfWork : IRestUnitOfWork
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

        public IAuthRepository AuthRepository
        {
            get
            {
                if(_authRepository == null)
                    _authRepository = new AuthRepository();
                return _authRepository;
            }
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
