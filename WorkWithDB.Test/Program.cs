using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using WebApp.Api.Models.Requests;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;

using WorkWithDB.DAL.Rest;
using WorkWithDB.DAL.Rest.Repository;
using WorkWithDB.DAL.SqlServer;
using WorkWithDB.Entity;

namespace WorkWithDB.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (IUnitOfWork scope = new SqlServerAdoNetUnitOfWork())
            //{
            //    var id = scope.BlogUserRepository.Insert(new BlogUser() { Name = "Bogdan", Nick = "winnie2", UserPassword = "!QAZ2wsx#EDC4rfv" });
            //    var allUsers = scope.BlogUserRepository.GetAll();
            //     id = allUsers.Select(u => u.Id).FirstOrDefault();
            //    var user = scope.BlogUserRepository.GetById(id);

            //    scope.Commit();
            //}

            //var wc = HttpWebRequest.CreateHttp("http://localhost:17017/api/BlogPost/GetPostsOfCurrentUser?token=test");

            //wc.Method = "POST";
            
            //var webResponse = wc.GetResponse();

            
            
            //using (RestUnitOfWork scope = new RestUnitOfWork())
            //{
            //    //scope.AuthRepository.Register(new RegisterModel()
            //    //{
            //    //    Name = "Vasya Pupkin",
            //    //    Nick = "Vasya",
            //    //    Password = "qwerty"
            //    //});
            //    var user = scope.AuthRepository.Login( "user","qwerty");

            //    var posts = scope.BlogPostRepository.GetAllWithUserNick();
            //}

            var request = WebRequest.CreateHttp("http://localhost:17017/api/BlogPost/GetPostsOfCurrentUser?token=test");
            //request.Method = "POST";
            request.ContentType = "application/json";
            var webResponse = request.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            var streamReader = new StreamReader(responseStream);

            var data = streamReader.ReadToEnd();

            var deserializeObject = JsonConvert.DeserializeObject<Result<IList<BlogPost>>>(data);


            GetRquest();
            PostRquest();
        }

        private static void GetRquest()
        {
            var client = new RestClient("http://localhost:17017/api/");

            var restRequest = new RestRequest("BlogPost/GetPostsOfCurrentUser");
            restRequest.AddParameter("token", "test");

            var restResponse = client.Execute<Result<List<BlogPost>>>(restRequest);
        }
        private static void PostRquest()
        {
            var client = new RestClient("http://localhost:17017/api/");

            var restRequest = new RestRequest("BlogPost/Save", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("token", "test",ParameterType.QueryString);

            restRequest.AddBody(
                   //new {
                   //   UserId= 26,
                   //   Content= "sample string 2",
                   //   Title= "sample string 3",
                   //   Created= "2016-03-30T10:58:15.0263153+03:00",
                   //   Id= 0
                   // }
                   new BlogPost
                   {
                       UserId = 26,
                       Content = "sample string 2",
                       Title = "sample string 3",
                       Id = 0
                   }
                );

            var restResponse = client.Execute<Result<int>>(restRequest);
        }
    }
}
