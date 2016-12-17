using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WorkWithDB.DAL.EF
{
    internal static class DbContextFactory
    {
        public static BlogDbContext CreateContext()
        {
            string connectingString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            BlogDbContext context = new BlogDbContext(connectingString);
            return context;
        }
    }
}
