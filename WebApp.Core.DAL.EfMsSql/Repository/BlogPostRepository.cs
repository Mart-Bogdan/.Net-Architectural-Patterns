using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.DAL.Abstract;
using WebApp.Core.DAL.EfMsSql.Repository.Abstract;
using WebApp.Core.Entity.Entities;
using WebApp.Core.Entity.Views;

namespace WebApp.Core.DAL.EfMsSql.Repository
{
    class BlogPostRepository : BaseRepository<int, BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(BlogDbContext context) 
            : base(context) { }

        public BlogPostWithAuthor GetByIdWithAuthor(int id)
        {
            return Items
                .Where(post=>post.Id == id)
                .Select(post => new BlogPostWithAuthor
                {
                    Id = post.Id,
                    Content = post.Content,
                    Created = post.Created,
                    Title = post.Title,
                    AuthorNick = post.User.UserName,
                    UserId = post.UserId
                })
                .FirstOrDefault();
        }
        
        public BlogPostWithAuthor GetByIdWithAuthor2(int id)
        {

            var users = Context.Users;
            var posts = Context.BlogPosts;

            var query = from post in posts
                join user in users on  post.UserId equals user.Id 
                select new BlogPostWithAuthor
                {
                    Id = post.Id,
                    Content = post.Content,
                    Created = post.Created,
                    Title = post.Title,
                    AuthorNick =user.UserName,
                    UserId = user.Id
                };
            
            return query.FirstOrDefault();
        }
        
        public BlogPostWithAuthor GetByIdWithAuthor3(int id)
        {
            var post = Items.Where(p => p.Id == id).Include(p=>p.User).FirstOrDefault();
            if (post == null)
            {
                return null;
            }

            return new BlogPostWithAuthor
            {
                Id = post.Id,
                Content = post.Content,
                Created = post.Created,
                Title = post.Title,
                AuthorNick = post.User.UserName,
                UserId = post.User.Id
            };
        }

        public IList<BlogPost> GetByUserId(string userId)
        {
            return Items.Where(p => p.UserId == userId).ToList();
        }

        public int GetCountByUserId(string userId)
        {
            return Items.Where(p => p.UserId == userId).ToList().Count;
        }

        
        public IList<BlogPostWithAuthor> GetAllWithUserNick()
        {

            var users = Context.Users;
            var posts = Context.BlogPosts;

            var query = from post in posts
                join user in users on  post.UserId equals user.Id 
                select new BlogPostWithAuthor
                {
                    Id = post.Id,
                    Content = post.Content,
                    Created = post.Created,
                    Title = post.Title,
                    AuthorNick =user.UserName,
                    UserId = user.Id
                };
            
            return query.ToList();
        }
        
        public IList<BlogPostWithAuthor> GetAllWithUserNick2()
        {
            return Items.Select(post => new BlogPostWithAuthor()
            {
                AuthorNick = Context.Set<BlogUser>().First(u => Equals(u.Id, post.UserId)).UserName,
                Content = post.Content,
                Created = post.Created, Id = post.Id,
                Title = post.Title, UserId = post.UserId
            }).ToList();
        }

        public override bool Update(BlogPost entity)
        {
            var item = Items.FirstOrDefault(e => Equals(e.Id, entity.Id));
            if (item == null)
                return false;
            item.Content = entity.Content;
            item.Created = entity.Created;
            item.Title = entity.Title;
            item.UserId = entity.UserId;
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }
    }
}