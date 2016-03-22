using System;
using System.Web.Http;
using WebApp.Abstract;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;

namespace WebApp.Controllers.Api
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public AuthResult Login(LoginModel model)
        {
            if(model==null)
                throw new ArgumentNullException("model");

            var userId = BlFactory.CredentialsChecker.CheckUserExist(model.Nick, model.Password);
            if (userId.HasValue)
            {
                var token = BlFactory.AccessTokenGenerator.GenerateToken(userId.Value, model.Nick);

                return new AuthResult {Token = token, Message = "Ok"};
            }

            return new AuthResult {Message = "Unauthorized!"};
        }


        [HttpPost]
        public AuthResult Register(RegisterModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");


            if(String.IsNullOrWhiteSpace(model.Name)||String.IsNullOrWhiteSpace(model.Nick)||string.IsNullOrWhiteSpace(model.Password))
                return new AuthResult { Message = "Incorrect fields!" };

            using (var uow = UnitOfWorkFactory.CreateInstance())
            {
                var userRepository = uow.BlogUserRepository;
                var user = new BlogUser {Nick = model.Nick, UserPassword = model.Password,Name = model.Name};
                var userId = userRepository.Insert(user);
                if (userId > 0)
                {
                    uow.Commit();

                    var token = BlFactory.AccessTokenGenerator.GenerateToken(userId, model.Nick);

                    return new AuthResult { Token = token, Message = "Ok" };
                }
            }

            return new AuthResult { Message = "Can't save user!" };
        }
    }
}
