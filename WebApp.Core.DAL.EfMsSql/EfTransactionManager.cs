using System;
using Microsoft.EntityFrameworkCore.Storage;
using WebApp.Core.DAL.Abstract;

namespace WebApp.Core.DAL.EfMsSql
{
    class EfTransactionManager : ITransactionManager
    {
        private readonly BlogDbContext _context;
        private IDbContextTransaction _transaction;

        public IDbContextTransaction CurrentTransaction => _transaction;

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