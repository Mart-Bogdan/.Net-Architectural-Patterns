using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Abstract.Security;

namespace WebApp.Abstract
{
    public static class BlFactory
    {
        // ReSharper disable once InconsistentNaming
        public static class __Config
        {
            public static void InitializeAccessTokenGenerator(IAccessTokenGenerator generator)
            {
                AccessTokenGenerator = generator;
            }
            public static void InitializeAccessTokenValidator(IAccessTokenValidator validator)
            {
                AccessTokenValidator = validator;
            }

            public static void InitializeCredentialsChecker(ICredentialsChecker credentialsChecker)
            {
                BlFactory.CredentialsChecker = credentialsChecker;
            }
        }

        public static ICredentialsChecker CredentialsChecker { get; private set; }
        public static IAccessTokenGenerator AccessTokenGenerator { get; private set; }
        public static IAccessTokenValidator AccessTokenValidator { get; private set; }
    }
}
