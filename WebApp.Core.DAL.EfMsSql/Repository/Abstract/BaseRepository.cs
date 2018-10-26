using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.DAL.Abstract;
using WebApp.Core.Entity.Entities;

namespace WebApp.Core.DAL.EfMsSql.Repository.Abstract
{
    internal abstract class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> 
        where TEntity :class,  IBaseEntity<TKey>
        where TKey : IComparable
    {
        private readonly BlogDbContext _context;
        protected BaseRepository(BlogDbContext context)
        {
            _context = context;
            Items = _context.Set<TEntity>();
        }

        protected BlogDbContext Context
        {
            get { return _context; }
        }
        protected DbSet<TEntity> Items { get; set; }

        public bool Delete(TKey id)
        {
            TEntity entity = GetById(id);
            if (entity == null)
                return false;
            Items.Remove(entity);
            Context.SaveChanges();
            return true;
        }

        public IList<TEntity> GetAll()
        {
            return Items.ToList();
        }

        public TEntity GetById(TKey id)
        {
            return Items.Find(id);
        }

        public int GetCount()
        {
            return Items.Count();
        }

        public TKey Insert(TEntity entity)
        {
            Items.Add(entity);
            Context.SaveChanges();
            return entity.Id;
        }

        public abstract bool Update(TEntity entity);

        public TKey Upsert(TEntity entity)
        {
            if (Object.Equals(entity.Id, default(TKey)))
                return Insert(entity);
            else
            {
                return Update(entity) ? entity.Id : default(TKey);
            }
        }
    }
}