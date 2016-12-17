using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.EF.Repository;

namespace WorkWithDB.DAL.EF
{
    class EFUnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _context;

        private IBlogUserRepository _blogUserRepository;
        private IBlogPostRepository _blogPostRepository;
        private IAuthRepository _authRepository;

        public EFUnitOfWork()
        {
            _context = DbContextFactory.CreateContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if (_blogPostRepository == null)
                    _blogPostRepository = new BlogPostRepository(_context);
                return _blogPostRepository;
            }
        }

        public IBlogUserRepository BlogUserRepository
        {
            get
            {
                if (_blogUserRepository == null)
                    _blogUserRepository = new BlogUserRepository(_context);
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

        public ITransactionManager TransactionManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
