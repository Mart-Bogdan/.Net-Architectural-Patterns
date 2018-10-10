using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Core.Abstract.Security;
using WorkWithDB.DAL.Standard.Abstract;

namespace WebApp.Core.BL.Security
{
    public class CredentialsChecker : ICredentialsChecker
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
