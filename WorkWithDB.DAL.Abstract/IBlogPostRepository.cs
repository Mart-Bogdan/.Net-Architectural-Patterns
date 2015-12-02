using System.Collections.Generic;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Views;

namespace WorkWithDB.DAL.Abstract
{
    public interface IBlogPostRepository : IBaseRepository<int, BlogPost>
    {
        IList<BlogPost> GetByUserId(int userId);
        int GetCountByUserId(int userId);
        IList<BlogPostWithAuthor> GetAllWithUserNick();
    }
}
