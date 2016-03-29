using System;

namespace WorkWithDB.DAL.Abstract.Rest
{
    public interface IRestUnitOfWork : IDisposable
    {
        IBlogPostRepository BlogPostRepository { get; }
        IAuthRepository AuthRepository { get; }
    }
}
