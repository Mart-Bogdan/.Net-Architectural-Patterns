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
        private readonly IBlogUserRepository _userRepository;

        public CredentialsChecker(IBlogUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int? CheckUserExist(string userName, string userPassword)
        {
            var blogUser = _userRepository.GetByLoginPassword(userName, userPassword);
            if (blogUser != null)
                return blogUser.Id;
            else
                return null;
        }
    }
}
