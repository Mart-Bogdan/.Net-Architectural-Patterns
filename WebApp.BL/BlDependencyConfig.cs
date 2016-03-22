using WebApp.Abstract;
using WebApp.BL.Security;

namespace WebApp.BL
{
    public static class BlDependencyConfig
    {
        public static void RegisterDependency()
        {
            {
                var tokenManager = new AccessTokenManager();

                BlFactory.__Config.InitializeAccessTokenGenerator(tokenManager);
                BlFactory.__Config.InitializeAccessTokenValidator(tokenManager);
            }

            BlFactory.__Config.InitializeCredentialsChecker(new CredentialsChecker());
        }
    }
}