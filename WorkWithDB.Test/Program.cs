using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithDB.Abstract;
using WorkWithDB.Entity;
using WorkWithDB.Repository;

namespace WorkWithDB.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IUnitOfWork scope = new UnitOfWork())
            {
                scope.BlogUserRepository.Insert(new BlogUser() { Name = "Evgeniy", Nick = "student_3121", UserPassword = "!QAZ2wsx#EDC4rfv" });
                
                scope.Commit();
            }

        }
    }
}
