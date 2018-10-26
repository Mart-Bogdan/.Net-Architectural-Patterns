using System;
using WebApp.Core.DAL.Abstract;
using WebApp.Core.DAL.EfMsSql.Repository.Abstract;
using WebApp.Core.Entity.Entities;

namespace WebApp.Core.DAL.EfMsSql.Repository
{
    class BlogUserRepository : BaseRepository<string, BlogUser>, IBlogUserRepository
    {
        public BlogUserRepository(BlogDbContext context) : base(context)
        {
            
        }

        public override bool Update(BlogUser entity)
        {
            throw new InvalidOperationException("Users should be created using UserManger!");
        }
    }
}