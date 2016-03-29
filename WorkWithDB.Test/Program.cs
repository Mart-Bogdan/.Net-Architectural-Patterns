using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Api.Models.Requests;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.Abstract.Rest;
using WorkWithDB.DAL.Rest;
using WorkWithDB.DAL.SqlServer;
using WorkWithDB.Entity;

namespace WorkWithDB.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (IUnitOfWork scope = new SqlServerAdoNetUnitOfWork())
            //{
            //    var id = scope.BlogUserRepository.Insert(new BlogUser() { Name = "Bogdan", Nick = "winnie2", UserPassword = "!QAZ2wsx#EDC4rfv" });
            //    var allUsers = scope.BlogUserRepository.GetAll();
            //     id = allUsers.Select(u => u.Id).FirstOrDefault();
            //    var user = scope.BlogUserRepository.GetById(id);

            //    scope.Commit();
            //}
            using (IRestUnitOfWork scope = new RestUnitOfWork())
            {
                //scope.AuthRepository.Register(new RegisterModel()
                //{
                //    Name = "Vasya Pupkin",
                //    Nick = "Vasya",
                //    Password = "qwerty"
                //});
                //scope.AuthRepository.Login(new LoginModel(){Nick = "user",Password = "qwerty"});

                scope.BlogPostRepository.GetPostsOfCurrentUser("");
            }
        }
    }
}
