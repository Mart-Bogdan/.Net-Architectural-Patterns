using System;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using RestSharpJsonNet;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract.Rest;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace WorkWithDB.DAL.Rest.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private ISerializer _serializer = new RestSharpJsonNetSerializer(new JsonSerializer());
        public AuthResult Login(LoginModel model)
        {
            var client = new RestClient("http://localhost:17017");

            var request = new RestRequest("api/Auth/Login", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(new {Nick = model.Nick, Password = model.Password});

            IRestResponse<AuthResult> response = client.Execute<AuthResult>(request);

            return response.Data;
        }

        public AuthResult Register(RegisterModel model)
        {
            var client = new RestClient("http://localhost:17017");

            var request = new RestRequest("api/Auth/Register", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddBody(new {Name = model.Name, Nick = model.Nick, Password = model.Password});

            IRestResponse<AuthResult> response = client.Execute<AuthResult>(request);

            return response.Data;
        }
    }
}
