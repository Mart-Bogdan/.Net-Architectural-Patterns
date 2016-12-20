using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;

namespace WorkWithDB.DAL.Abstract
{
    public interface IBlogUserRepository : IBaseRepository<int, BlogUser>
    {
        BlogUser GetByLoginPassword(string login, string password);
    }
}
