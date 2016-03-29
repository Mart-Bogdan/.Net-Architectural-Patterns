using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using RestSharpJsonNet;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace WorkWithDB.DAL.Rest.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private ISerializer _serializer = new RestSharpJsonNetSerializer(new JsonSerializer());

        public BlogUser Login(string login, string password)
        {
            var client = new RestClient("http://localhost:17017");

            var request = new RestRequest("api/Auth/Login", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(new { Nick = login, Password = password });

            IRestResponse<AuthResult> response = client.Execute<AuthResult>(request);

            ValidateResponce(response);
            return response.Data.User;
        }

        public BlogUser Register(BlogUser user)
        {
            var client = new RestClient("http://localhost:17017");

            var request = new RestRequest("api/Auth/Register", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(user.Clone());

            IRestResponse<AuthResult> response = client.Execute<AuthResult>(request);

            ValidateResponce(response);


            return response.Data.User;
        }

        private static void ValidateResponce(IRestResponse<AuthResult> response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
                if (response.ErrorException != null)
                    throw response.ErrorException;
                else throw new Exception(response.ErrorMessage);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);
        }
    }
}
