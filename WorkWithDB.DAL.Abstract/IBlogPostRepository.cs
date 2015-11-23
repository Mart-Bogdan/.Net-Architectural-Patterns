using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Entity;

namespace WorkWithDB.Abstract
{
    public interface IBlogPostRepository : IBaseRepository<int, BlogPost>
    {
        IList<BlogPost> GetAll();
        IList<BlogPost> GetByUserId(int userId);
        int GetCountByUserId(int userId);
    }
}
