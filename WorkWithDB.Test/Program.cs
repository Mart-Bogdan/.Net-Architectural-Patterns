using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.SqlServer;
using WorkWithDB.Entity;

namespace WorkWithDB.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IUnitOfWork scope = new UnitOfWork())
            {
                //var id = scope.BlogUserRepository.Insert(new BlogUser() { Name = "Evgeniy", Nick = "student_3121", UserPassword = "!QAZ2wsx#EDC4rfv" });
                var allUsers = scope.BlogUserRepository.GetAll();
                var id = allUsers.Select(u => u.Id).FirstOrDefault();
                var user = scope.BlogUserRepository.GetById(id);

                scope.Commit();
            }

        }
    }
}
