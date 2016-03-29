using System.Collections.Generic;
using RestSharp;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract.Rest;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Views;

namespace WorkWithDB.DAL.Rest.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public List<BlogPost> GetPostsOfCurrentUser(string token)
        {
            var client = new RestClient("http://localhost:17017");

            var request = new RestRequest("api/BlogPost/GetPostsOfCurrentUser", Method.GET);
            request.AddParameter("token", token);

            IRestResponse<List<BlogPost>> response = client.Execute<List<BlogPost>>(request);

            return response.Data;
        }

        public List<BlogPostWithAuthor> GetAllWithUserNick(string token)
        {
            throw new System.NotImplementedException();
        }

        public int Save(string token, BlogPost post)
        {
            throw new System.NotImplementedException();
        }
    }
}
