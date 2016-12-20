using System.Collections.Generic;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities.Abstract;

namespace WorkWithDB.DAL.Abstract
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
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
