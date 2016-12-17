using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.EF.Infrastructure;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Views;

namespace WorkWithDB.DAL.EF.Repository
{
    class BlogPostRepository : BaseRepository<int, BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(BlogDbContext context) 
            : base(context) { }

        public IList<BlogPost> GetByUserId(int userId)
        {
            return Items.Where(p => p.UserId == userId).ToList();
        }

        public int GetCountByUserId(int userId)
        {
            return Items.Where(p => p.UserId == userId).ToList().Count;
        }

        public IList<BlogPostWithAuthor> GetAllWithUserNick()
        {
            var posts = GetAll();
            return posts.Select(post => new BlogPostWithAuthor()
            {
                AuthorNick = Context.Set<BlogUser>().First(u => Equals(u.Id, post.UserId)).Nick,
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
