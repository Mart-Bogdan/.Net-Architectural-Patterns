using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Core.Abstract.Security
{
    public interface IAccessTokenValidator
    {
        [CanBeNull]
        UserDescriptor ValidateToken(string token);
    }
}
