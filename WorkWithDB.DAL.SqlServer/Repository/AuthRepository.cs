using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;

namespace WorkWithDB.DAL.SqlServer.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SqlServerAdoNetUnitOfWork _unit;

        public AuthRepository(SqlServerAdoNetUnitOfWork unit)
        {
            _unit = unit;
        }

        public BlogUser Login(string login, string password)
        {
            return _unit.BlogUserRepository.GetByLoginPassword(login, password);
        }

        public BlogUser Register(BlogUser user)
        {
            var id = _unit.BlogUserRepository.Insert(user);

            return _unit.BlogUserRepository.GetById(id);
        }
    }
}