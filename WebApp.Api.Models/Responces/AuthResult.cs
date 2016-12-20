using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;

namespace WebApp.Api.Models.Responces
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public BlogUser User { get; set; }
    }
}
