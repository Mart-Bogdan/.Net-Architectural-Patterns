using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using RestSharpJsonNet;
using WebApp.Api.Models.Responces;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.Rest.Infrastructure;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Views;

namespace WorkWithDB.DAL.Rest.Repository
{
    public class BlogPostRepository : BaseRestRepository<int, BlogPost>, IBlogPostRepository
    {
        public IList<BlogPostWithAuthor> GetAllWithUserNick()
        {
            var result = ExecuteRequest<List<BlogPostWithAuthor>>("BlogPost/GetAllWithUserNick");

            return result;
        }

        private int SaveImpl(BlogPost post)
        {
            return ExecuteRequest<int>("BlogPost/Save", body: post);
        }

        public override int Insert(BlogPost entity)
        {
            var post = entity.Clone();
            post.Id = 0;
            return SaveImpl(post);
        }

        public override bool Update(BlogPost entity)
        {
            if (entity.Id == 0)
                return false;

            try
            {
                var post = entity.Clone();
                return SaveImpl(post) == post.Id;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                    return false;
                throw;
            }
        }

        public override int Upsert(BlogPost entity)
        {
            return SaveImpl(entity.Clone());
        }

        public override int GetCount()
        {
            return ExecuteRequest<int>("BlogPost/Count");
        }

        public override BlogPost GetById(int id)
        {
            return ExecuteRequest<BlogPost>("BlogPost/Get", arguments: new { id });
        }

        public override bool Delete(int id)
        {
            return ExecuteRequest<bool>("BlogPost/Delete", arguments: new { id });
        }

        public override IList<BlogPost> GetAll()
        {
            return ExecuteRequest<List<BlogPost>>("BlogPost/GetAll");
        }

        //TODO No API endpoint
        public IList<BlogPost> GetByUserId(int userId)
        {
            return GetAll().Where(post => post.UserId == userId).ToList();
        }

        public int GetCountByUserId(int userId)
        {
            return ExecuteRequest<int>("BlogPost/CountByUserId", arguments: new { userId });
        }

    }
}
