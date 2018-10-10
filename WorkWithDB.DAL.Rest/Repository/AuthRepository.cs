using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using RestSharpJsonNet;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.Rest.Infrastructure;
using WorkWithDB.Entity;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace WorkWithDB.DAL.Rest.Repository
{
    public class AuthRepository : BaseRestOperations, IAuthRepository
    {
        private ISerializer _serializer = new RestSharpJsonNetSerializer(new JsonSerializer());

        public BlogUser Login(string login, string password)
        {
            var result = ExecuteRequestRaw<AuthResult>(
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
