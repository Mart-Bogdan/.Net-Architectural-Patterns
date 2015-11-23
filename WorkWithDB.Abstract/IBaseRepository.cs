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
        bool Update(TEntity entity);
        int Upsert(TEntity entity);

        int GetCount();

        TEntity GetById(TKey id);
        bool Delete(TKey id);
    }
}
