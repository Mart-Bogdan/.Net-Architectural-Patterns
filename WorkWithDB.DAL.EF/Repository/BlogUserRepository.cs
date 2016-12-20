using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.EF.Infrastructure;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;

namespace WorkWithDB.DAL.EF.Repository
{
    class BlogUserRepository : BaseRepository<int, BlogUser>, IBlogUserRepository
    {
        public BlogUserRepository(BlogDbContext context) 
            : base(context) { }

        public BlogUser GetByLoginPassword(string login, string password)
        {
            return Items.First(u => u.Nick == login && u.UserPassword == password);
        }

        public override bool Update(BlogUser entity)
        {
            var item = Items.FirstOrDefault(e => Equals(e.Id, entity.Id));
            if (item == null)
                return false;
            item.Name = entity.Name;
            item.Nick = entity.Nick;
            item.UserPassword = entity.UserPassword;
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
            return true;
        }
    }
}
