using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace WorkWithDB.DAL.EF
{
    internal static class DbContextFactory
    {
        public static BlogDbContext CreateContext()
        {
            string connectingString = ConfigurationManager.ConnectionStrings["BlogDbContext"].ConnectionString;
            BlogDbContext context = new BlogDbContext(connectingString);
            return context;
        }
    }
}
