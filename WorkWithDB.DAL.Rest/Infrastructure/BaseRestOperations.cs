using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using RestSharp;
using WebApp.Api.Models.Responces;

namespace WorkWithDB.DAL.Rest.Infrastructure
{
    public class BaseRestOperations
    {
        static readonly string ApiUrl = ConfigurationManager.AppSettings["api_endpopint"] ?? "http://localhost:17017/api/";

        protected RestClient Client;

        public BaseRestOperations()
        {
            Client = new RestClient(ApiUrl);
        }

        private static RestRequest BuildRequest(string resource, object body, object arguments)
        {
            var request = new RestRequest(resource, body == null ? Method.GET : Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            //if (TokenHolder.AuthToken != null)
            request.AddParameter("token", TokenHolder.AuthToken);

            if (body != null) 
                request.AddBody(body);

            if (arguments != null)
            {
                Dictionary<string,object> dict = AnonObjectToDictionary(arguments);

                foreach (var kv in dict)
                {
                    request.AddParameter(kv.Key, kv.Value);
                }
            }

            return request;
        }


        protected T ExecuteRequestRaw<T>(string resource, object body = null, object arguments = null) where T : new()
        {
            var request = BuildRequest(resource, body, arguments);

            IRestResponse<T> response = Client.Execute<T>(request);

            ValidateResponceRaw(response);

            var result = response.Data;
            return result;
        }

        /// <summary>
        /// Parsers resources that wraps <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource"></param>
        /// <param name="body"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        protected T ExecuteRequest<T>(string resource, object body=null,object arguments=null)
        {
            var request = BuildRequest(resource, body, arguments);

            IRestResponse<Result<T>> response = Client.Execute<Result<T>>(request);

            ValidateResponce(response);

            var result = response.Data;
            return result.Value;
        }

        private void ValidateResponce<T>(IRestResponse<Result<T>> response)
        {
            ValidateResponceRaw(response);

            var result = response.Data;
            if(result.HasError)
                throw new Exception(result.ErrorMessage);
        }

        private static void ValidateResponceRaw(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
                if (response.ErrorException != null)
                    throw response.ErrorException;
                else
                    throw new Exception(response.ErrorMessage);

            if (!String.IsNullOrEmpty(response.ErrorMessage))
                throw new Exception(response.ErrorMessage);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(response.Content);
        }

        private static Dictionary<string, object> AnonObjectToDictionary(object a)
        {
            var type = a.GetType();
            var props = type.GetProperties();
            //TODO this can be cahced as in here:
            // https://github.com/Mart-Bogdan/dotliquid/blob/13f0407550f960980805222a3c8b7ffdcd3b6f08/src/DotLiquid/Hash.cs#L49
            var pairs = props.ToDictionary(
                prop => prop.Name,
                prop => prop.GetValue(a)
                );

            return pairs;
        }
    }
}