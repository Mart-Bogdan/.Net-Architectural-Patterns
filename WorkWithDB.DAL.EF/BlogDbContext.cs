using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Entity;
using WorkWithDB.Entity.Entities;

namespace WorkWithDB.DAL.EF
{
    class BlogDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().ToTable("BlogPost");
            modelBuilder.Entity<BlogUser>().ToTable("BlogUser");
        }

        public BlogDbContext(string connectionString) : base(connectionString)
        {
        }

        public BlogDbContext()
        { }
    }
}
