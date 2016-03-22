using JetBrains.Annotations;

namespace WebApp.Abstract.Security
{
    public interface IAccessTokenValidator
    {
        [CanBeNull]
        UserDescriptor ValidateToken(string token);
    }
}