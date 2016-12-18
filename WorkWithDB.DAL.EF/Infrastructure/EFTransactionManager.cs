using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.DAL.Abstract;

namespace WorkWithDB.DAL.EF.Infrastructure
{
    class EFTransactionManager : ITransactionManager
    {
        private readonly BlogDbContext _context;
        private DbContextTransaction _transaction;

        public DbContextTransaction CurrentTransaction => _transaction;

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            _transaction = null;
        }

        public IDisposable Begin()
        {
            _transaction?.Dispose();
            _transaction = _context.Database.BeginTransaction();
            return this;
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void RollBack()
        {
            _transaction?.Rollback();
            _transaction = null;
        }
    }
}
