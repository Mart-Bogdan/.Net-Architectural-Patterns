﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Abstract;
using WorkWithDB.Entity;

namespace WorkWithDB.Repository
{
    public class BlogCommentRepository : IBlogCommentRepository 
    {
        public int Insert(Entity.BlogComment entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Entity.BlogComment entity)
        {
            throw new NotImplementedException();
        }

        public void Upsert(BlogComment entity)
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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
