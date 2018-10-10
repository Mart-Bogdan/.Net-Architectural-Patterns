using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Abstract.Security;
using WebApp.Standard.Api.Models.Requests;
using WebApp.Standard.Api.Models.Responces;
using WorkWithDB.DAL.Standard.Abstract;
using BlogUser = WorkWithDB.Standard.Entity.Entities.BlogUser;

namespace WebAppCore.Controllers
{
    public class AuthController : Controller
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
            if (model == null)
                throw new ArgumentNullException("model");

            var userId = _credentialsChecker.CheckUserExist(model.Nick, model.Password);
            if (userId.HasValue)
            {
                var token = _accessTokenGenerator.GenerateToken(userId.Value, model.Nick);

                BlogUser blogUser = _blogUserRepository.GetById(userId.Value);

                return new AuthResult { Token = token, Message = "Ok", User = blogUser };
            }

            return new AuthResult { Message = "Unauthorized!" };
        }


        [HttpPost]
        public AuthResult Register(RegisterModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (String.IsNullOrWhiteSpace(model.Name) || String.IsNullOrWhiteSpace(model.Nick) ||
                string.IsNullOrWhiteSpace(model.Password))
                return new AuthResult { Message = "Incorrect fields!" };

            var user = new BlogUser { Nick = model.Nick, UserPassword = model.Password, Name = model.Name };
            var userId = _blogUserRepository.Insert(user);
            if (userId > 0)
            {
                var token = _accessTokenGenerator.GenerateToken(userId, model.Nick);

                var blogUser = _blogUserRepository.GetById(userId);

                return new AuthResult { Token = token, Message = "Ok", User = blogUser };
            }

            return new AuthResult { Message = "Can't save user!" };
        }
    }
}
