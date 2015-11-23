using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Entity;

namespace WorkWithDB.Abstract
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        TKey Insert(TEntity entity);
        void Update(TEntity entity);
        void Upsert(TEntity entity);

        int GetCount();

        TEntity GetById(TKey id);
        void Delete(TKey id);
    }
}
