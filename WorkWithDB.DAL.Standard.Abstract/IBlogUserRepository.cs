using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.Standard.Entity.Entities;

namespace WorkWithDB.DAL.Standard.Abstract
{
    public interface IBlogUserRepository : IBaseRepository<int, BlogUser>
    {
        BlogUser GetByLoginPassword(string login, string password);
    }
}
