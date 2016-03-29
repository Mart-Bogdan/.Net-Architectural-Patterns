using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;

namespace WorkWithDB.DAL.Abstract.Rest
{
    public interface IAuthRepository
    {
        AuthResult Login(LoginModel model);
        AuthResult Register(RegisterModel model);
    }
}
