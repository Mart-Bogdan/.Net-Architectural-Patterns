using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.DAL.Standard.Abstract;

namespace WorkWithDB.DAL.Standard.Rest.Infrastructure
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
