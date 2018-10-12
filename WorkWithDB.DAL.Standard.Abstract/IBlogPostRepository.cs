using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.Standard.Entity.Entities;
using WorkWithDB.Standard.Entity.Views;


namespace WorkWithDB.DAL.Standard.Abstract
{
    public interface IBlogPostRepository : IBaseRepository<int, BlogPost>
    {
        BlogPostWithAuthor GetByIdWithAuthor(int id);
        IList<BlogPost> GetByUserId(int userId);
        int GetCountByUserId(int userId);
        IList<BlogPostWithAuthor> GetAllWithUserNick();
    }
}
