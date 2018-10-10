using System;
using WorkWithDB.DAL.Abstract;

namespace WorkWithDB.DAL.Rest.Infrastructure
{
    /// <summary>
    /// All methods do nothing, as REST don't suppost transactions
    /// </summary>
    public class DummyTxManager : ITransactionManager
    {
        public void Dispose() { }

        public IDisposable Begin()
        {
            return this;
        }

        public void Commit() { }

        public void RollBack() { }
    }
}