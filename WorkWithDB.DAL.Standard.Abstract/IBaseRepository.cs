using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.Standard.Entity.Entities.Abstract;

namespace WorkWithDB.DAL.Standard.Abstract
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        TKey Insert(TEntity entity);
        bool Update(TEntity entity);
        int Upsert(TEntity entity);

        int GetCount();

        TEntity GetById(TKey id);
        bool Delete(TKey id);

        IList<TEntity> GetAll();
    }
}
