using System;
using System.Collections.Generic;
using System.Text;

namespace WorkWithDB.DAL.Standard.Abstract
{

    [Obsolete("This interface is left for use by non DI aware clients, like our UI application")]
    public interface IUnitOfWork : IDisposable
    {
        IBlogPostRepository BlogPostRepository { get; }
        IBlogUserRepository BlogUserRepository { get; }
        IAuthRepository AuthRepository { get; }
        ITransactionManager TransactionManager { get; }
    }
}
