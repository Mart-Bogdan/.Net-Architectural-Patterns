using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Abstract.Security
{
    public interface IAccessTokenGenerator
    {
        string GenerateToken(int userId, string nick);
    }
}
