using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.EF.Infrastructure;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;

namespace WorkWithDB.DAL.EF.Repository
{
    class AuthRepository : IAuthRepository
    {
        private readonly IBlogUserRepository _blogUserRepository;

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
