using System.Collections.Generic;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;

namespace WorkWithDB.DAL.Rest.Infrastructure
{
    public abstract class BaseRestRepository<TKey, TEntity> : BaseRestOperations, IBaseRepository<TKey, TEntity> 
        where TEntity : BaseEntity<TKey>
    {
        //TODO эти методы могут быть реализованы в общем виде, но нам
        public abstract TKey Insert(TEntity entity);
        public abstract bool Update(TEntity entity);
        public abstract int Upsert(TEntity entity);
        public abstract int GetCount();
        public abstract TEntity GetById(TKey id);
        public abstract bool Delete(TKey id);
        public abstract IList<TEntity> GetAll();
    }
}