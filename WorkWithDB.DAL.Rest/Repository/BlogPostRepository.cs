using System.Collections.Generic;
using RestSharp;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;
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

            var response = client.Execute<Result<List<BlogPost>>>(request);

            return response.Data.Value;
        }

        public List<BlogPostWithAuthor> GetAllWithUserNick(string token)
        {
            throw new System.NotImplementedException();
        }

        public int Save(string token, BlogPost post)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(BlogPost entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(BlogPost entity)
        {
            throw new System.NotImplementedException();
        }

        public int Upsert(BlogPost entity)
        {
            throw new System.NotImplementedException();
        }

        public int GetCount()
        {
            throw new System.NotImplementedException();
        }

        public BlogPost GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<BlogPost> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IList<BlogPost> GetByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public int GetCountByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IList<BlogPostWithAuthor> GetAllWithUserNick()
        {
            throw new System.NotImplementedException();
        }
    }
}
