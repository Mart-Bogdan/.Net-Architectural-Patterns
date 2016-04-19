using System;

namespace WorkWithDB.DAL.Abstract
{
    public interface ITransactionManager : IDisposable
    {
        void Begin();
        void Commit();
        void RollBack();
    }
}