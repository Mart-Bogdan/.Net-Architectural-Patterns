using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;

namespace WorkWithDB.DAL.Abstract
{
    public interface IAuthRepository
    {
        BlogUser Login(string login, string password);
        BlogUser Register(BlogUser user);
    }
}