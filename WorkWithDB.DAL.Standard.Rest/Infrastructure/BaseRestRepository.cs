using System;
using System.Collections.Generic;
using System.Text;
using WorkWithDB.DAL.Standard.Abstract;
using WorkWithDB.Standard.Entity.Entities.Abstract;


namespace WorkWithDB.DAL.Standard.Rest.Infrastructure
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
