using System;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.Entity;

namespace WorkWithDB.DAL.SqlServer.Repository
{
    public class BlogCommentRepository : IBlogCommentRepository 
    {
        public int Insert(Entity.BlogComment entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Entity.BlogComment entity)
        {
            throw new NotImplementedException();
        }

        public int Upsert(BlogComment entity)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public Entity.BlogComment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
