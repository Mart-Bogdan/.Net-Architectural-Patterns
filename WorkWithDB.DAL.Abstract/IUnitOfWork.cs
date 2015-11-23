﻿using System;

namespace WorkWithDB.DAL.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogPostRepository BlogPostRepository { get; }
        IBlogUserRepository BlogUserRepository { get; }
        void Commit();
        void RollBack();
    }
}