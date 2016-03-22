﻿using System;
using System.Web.Http;
using WebApp.Abstract;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;

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

    }
}
