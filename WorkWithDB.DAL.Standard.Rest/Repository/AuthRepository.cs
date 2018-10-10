using System;
using System.Collections.Generic;
using System.Text;
using RestSharp.Serializers;
using RestSharpJsonNet.Standard;
using WebApp.Standard.Api.Models.Responces;
using WorkWithDB.DAL.Standard.Abstract;
using WorkWithDB.DAL.Standard.Rest.Infrastructure;
using WorkWithDB.Standard.Entity.Entities;

namespace WorkWithDB.DAL.Standard.Rest.Repository
{
    public class AuthRepository : BaseRestOperations, IAuthRepository
    {
        private ISerializer _serializer = new RestSharpJsonNetSerializer(new Newtonsoft.Json.JsonSerializer());

        public BlogUser Login(string login, string password)
        {
            AuthResult result = ExecuteRequestRaw<AuthResult>(
                "Auth/Login",
                new
                {
                    Nick = login,
                    Password = password
                }
            );

            TokenHolder.AuthToken = result.Token;

            return result.User;
        }

        public BlogUser Register(BlogUser user)
        {
            var result = ExecuteRequestRaw<AuthResult>(
                "Auth/Register",
                user.Clone()
            );
            TokenHolder.AuthToken = result.Token;

            return result.User;
        }
    }
}
