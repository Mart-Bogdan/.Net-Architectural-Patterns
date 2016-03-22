using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monads.NET;
using WebApp.Abstract.Security;
using WorkWithDB.DAL.Abstract;

namespace WebApp.BL.Security
{
    class CredentialsChecker : ICredentialsChecker
    {
        public int? CheckUserExist(string userName, string userPassword)
        {
            using (var unitOfWork = UnitOfWorkFactory.CreateInstance())
            {
                var blogUser = unitOfWork.BlogUserRepository.GetByLoginPassword(userName, userPassword);
                if (blogUser != null)
                    return blogUser.Id;
                else
                    return null;
            }
        }
    }
}
