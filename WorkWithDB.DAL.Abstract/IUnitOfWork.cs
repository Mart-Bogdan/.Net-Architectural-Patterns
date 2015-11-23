using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WorkWithDB.Abstract
{
    public interface IUnitOfWork : IDisposable
    {

        IBlogCommentRepository BlogCommentRepository { get; }
        IBlogPostRepository BlogPostRepository { get; }
        IBlogUserRepository BlogUserRepository { get; }
        void Commit();
        void RollBack();
    }
}
