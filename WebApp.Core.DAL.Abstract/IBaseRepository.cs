using System.Collections.Generic;
using WebApp.Core.Entity.Entities;

namespace WebApp.Core.DAL.Abstract
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : IBaseEntity<TKey> 
    {
        TKey Insert(TEntity entity);
        bool Update(TEntity entity);
        TKey Upsert(TEntity entity);

        int GetCount();

        TEntity GetById(TKey id);
        bool Delete(TKey id);

        IList<TEntity> GetAll();
    }
}
