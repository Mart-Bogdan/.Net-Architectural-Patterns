using System;
using System.Collections.Generic;
using System.Text;

namespace WorkWithDB.DAL.Standard.Abstract
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
