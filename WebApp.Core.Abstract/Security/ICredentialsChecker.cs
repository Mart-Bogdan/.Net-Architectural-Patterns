using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Abstract.Security
{
    public interface ICredentialsChecker
    {
        int? CheckUserExist(string userName, string userPassword);
    }
}
