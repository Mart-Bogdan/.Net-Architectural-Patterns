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
        private IUnitOfWork uow = UnitOfWorkFactory.CreateInstance();

        [HttpPost]
        public AuthResult Login(LoginModel model)
        {
            if(model==null)
                throw new ArgumentNullException("model");

            var userId = BlFactory.CredentialsChecker.CheckUserExist(model.Nick, model.Password);
            if (userId.HasValue)
            {
                var token = BlFactory.AccessTokenGenerator.GenerateToken(userId.Value, model.Nick);

                var blogUser = uow.BlogUserRepository.GetById(userId.Value);
                
                return new AuthResult {Token = token, Message = "Ok", User = blogUser};
            }

            return new AuthResult {Message = "Unauthorized!"};
        }


        [HttpPost]
        public AuthResult Register(RegisterModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");


            if (String.IsNullOrWhiteSpace(model.Name) || String.IsNullOrWhiteSpace(model.Nick) ||
                string.IsNullOrWhiteSpace(model.Password))
                return new AuthResult {Message = "Incorrect fields!"};

            var userRepository = uow.BlogUserRepository;
            var user = new BlogUser {Nick = model.Nick, UserPassword = model.Password, Name = model.Name};
            var userId = userRepository.Insert(user);
            if (userId > 0)
            {
                uow.Commit();

                var token = BlFactory.AccessTokenGenerator.GenerateToken(userId, model.Nick);

                var blogUser = uow.BlogUserRepository.GetById(userId);
                
                return new AuthResult {Token = token, Message = "Ok", User = blogUser};
            }

            return new AuthResult {Message = "Can't save user!"};
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                uow.Dispose();

            base.Dispose(disposing);
        }
    }
}
