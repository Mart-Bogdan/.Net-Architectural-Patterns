using WorkWithDB.Entity;

namespace WorkWithDB.DAL.Abstract
{
    public interface IAuthRepository
    {
        BlogUser Login(string login, string password);
        BlogUser Register(BlogUser user);
    }
}