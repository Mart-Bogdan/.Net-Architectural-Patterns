using System;
using System.Collections.Concurrent;
using WebApp.Abstract;
using WebApp.Abstract.Security;

namespace WebApp.BL.Security
{
    internal class UserDescriptorHolder
    {
        public readonly UserDescriptor Descriptor;
        
        public DateTime LastAccessTime { get; private set; }
        
        public UserDescriptorHolder(UserDescriptor descriptor)
        {
            Descriptor = descriptor;
            LastAccessTime = DateTime.Now;
        }

        public bool ValidateAccessTime()
        {
            var now = DateTime.Now;

            if (LastAccessTime + AccessTokenManager.UserSessionWindow > now)
            {
                LastAccessTime = now;
                return true;
            }

            return false;
        }
    }
    public class AccessTokenManager:IAccessTokenGenerator,IAccessTokenValidator
    {
        internal static readonly TimeSpan UserSessionWindow = TimeSpan.FromMinutes(30);

        private readonly ConcurrentDictionary<Guid,UserDescriptorHolder> _tokensCache = new ConcurrentDictionary<Guid, UserDescriptorHolder>();

        public string GenerateToken(int userId, string nick)
        {
            var token = Guid.NewGuid();

            _tokensCache[token] = new UserDescriptorHolder(new UserDescriptor(userId,nick));

            return token.ToString("N");
        }

        public UserDescriptor ValidateToken(string tokenStr)
        {
            //TODO remove in production
            if(tokenStr == "test")
                return new UserDescriptor(26,"user");

            Guid token;
            if (Guid.TryParse(tokenStr, out token))
            {
                UserDescriptorHolder descrHolder;
                if (_tokensCache.TryGetValue(token, out descrHolder))
                {
                    if (descrHolder.ValidateAccessTime())
                    {
                        return descrHolder.Descriptor;
                    }
                    else
                    {
                        _tokensCache.TryRemove(token, out descrHolder);
                    }
                }
            }
            return null;
        }
    }
}