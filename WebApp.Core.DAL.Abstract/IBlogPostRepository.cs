using System.Collections.Generic;
using WebApp.Core.Entity.Entities;
using WebApp.Core.Entity.Views;

namespace WebApp.Core.DAL.Abstract
{
    public interface IBlogPostRepository : IBaseRepository<int, BlogPost>
    {
        BlogPostWithAuthor GetByIdWithAuthor(int id);
        IList<BlogPost> GetByUserId(string userId);
        int GetCountByUserId(string userId);
        IList<BlogPostWithAuthor> GetAllWithUserNick();
    }
}
