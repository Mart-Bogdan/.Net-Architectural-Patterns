using System;
using System.Web.Http;
using WebApp.Abstract;
using WebApp.Abstract.Security;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;

namespace WebApp.Controllers.Api
{
    public class AuthController : ApiController
    {
        private readonly ICredentialsChecker _credentialsChecker;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IBlogUserRepository _blogUserRepository;

        public AuthController(
            ICredentialsChecker credentialsChecker,
            IAccessTokenGenerator accessTokenGenerator, 
            IBlogUserRepository blogUserRepository
            )
        {
            _credentialsChecker = credentialsChecker;
            _accessTokenGenerator = accessTokenGenerator;
            _blogUserRepository = blogUserRepository;
        }

        [HttpPost]
        public AuthResult Login(LoginModel model)
        {
            if(model==null)
                throw new ArgumentNullException("model");

            var userId = _credentialsChecker.CheckUserExist(model.Nick, model.Password);
            if (userId.HasValue)
            {
                var token = _accessTokenGenerator.GenerateToken(userId.Value, model.Nick);

                var blogUser = _blogUserRepository.GetById(userId.Value);
                
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

            var user = new BlogUser {Nick = model.Nick, UserPassword = model.Password, Name = model.Name};
            var userId = _blogUserRepository.Insert(user);
            if (userId > 0)
            {
                var token = _accessTokenGenerator.GenerateToken(userId, model.Nick);

                var blogUser = _blogUserRepository.GetById(userId);
                
                return new AuthResult {Token = token, Message = "Ok", User = blogUser};
            }

            return new AuthResult {Message = "Can't save user!"};
        }
    }
}
