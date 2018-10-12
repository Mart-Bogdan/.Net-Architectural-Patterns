using WorkWithDB.Standard.Entity.Entities;

namespace WorkWithDB.Standard.Entity.Views
{
    /// <summary>
    /// This class represents posts and their author's nicks to display in list of posts
    /// </summary>
    public class BlogPostWithAuthor : BlogPost
    {
        public string AuthorNick { get; set; }

        public override string ToString()
        {
            return string.Format("BlogPostWithAuthor(Id: {3}, Created: {1}, UserId: {2}, Author: {4})", null, Created, UserId, Id, AuthorNick);
        }
    }
}
