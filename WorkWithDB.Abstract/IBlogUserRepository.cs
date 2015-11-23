using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Entity;

namespace WorkWithDB.Abstract
{
    public interface IBlogUserRepository : IBaseRepository<int, BlogUser>
    {
        List<BlogPost> GetUsersPost(int userId); 
    }
}
