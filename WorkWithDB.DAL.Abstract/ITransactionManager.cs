using System;

namespace WorkWithDB.DAL.Abstract
{
    public interface ITransactionManager : IDisposable
    {
        /// <summary>
        /// Returned disposable will auto close TX (rollback if not commited)
        /// </summary>
        /// <returns></returns>
        IDisposable Begin();
        void Commit();
        void RollBack();
    }
}