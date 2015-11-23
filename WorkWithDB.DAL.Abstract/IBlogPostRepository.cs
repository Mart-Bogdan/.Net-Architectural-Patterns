using System.Collections.Generic;
using WorkWithDB.Entity;

namespace WorkWithDB.DAL.Abstract
{
    public interface IBlogPostRepository : IBaseRepository<int, BlogPost>
    {
        IList<BlogPost> GetByUserId(int userId);
        int GetCountByUserId(int userId);
    }
}
