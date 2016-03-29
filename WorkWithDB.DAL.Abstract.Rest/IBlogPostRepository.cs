using System.Collections.Generic;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Views;

namespace WorkWithDB.DAL.Abstract.Rest
{
    public interface IBlogPostRepository
    {
        List<BlogPost> GetPostsOfCurrentUser(string token);
        List<BlogPostWithAuthor> GetAllWithUserNick(string token);
        int Save(string token, BlogPost post);
    }
}
