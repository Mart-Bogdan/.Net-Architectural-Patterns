using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;

namespace WorkWithDB.DAL.SqlServer.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private IBlogUserRepository _blogUserRepository;

        public AuthRepository(IBlogUserRepository blogUserRepository)
        {
            _blogUserRepository = blogUserRepository;
        }

        public BlogUser Login(string login, string password)
        {
            return _blogUserRepository.GetByLoginPassword(login, password);
        }

        public BlogUser Register(BlogUser user)
        {
            var id = _blogUserRepository.Insert(user);

            return _blogUserRepository.GetById(id);
        }
    }
}