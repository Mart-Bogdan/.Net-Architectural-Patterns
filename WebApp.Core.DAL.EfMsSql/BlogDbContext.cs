using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.Entity;
using WebApp.Core.Entity.Entities;

namespace WebApp.Core.DAL.EfMsSql
{
    public class BlogDbContext : IdentityDbContext<BlogUser>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<BlogPost> BlogPosts { get; protected set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}