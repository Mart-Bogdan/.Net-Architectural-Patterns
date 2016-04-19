using Ninject;
using Ninject.Modules;
using WebApp.Abstract.Security;
using WebApp.BL.Security;

namespace WebApp.BL
{
    public class BussinessLogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICredentialsChecker>().To<CredentialsChecker>();
            Bind<AccessTokenManager>().To<AccessTokenManager>().InSingletonScope();

            Bind<IAccessTokenGenerator>().ToMethod(ctx => ctx.Kernel.Get<AccessTokenManager>()).InSingletonScope();
            Bind<IAccessTokenValidator>().ToMethod(ctx => ctx.Kernel.Get<AccessTokenManager>()).InSingletonScope();


            /**
             * We can use following syntax for our "shard" singletones
             * But this will break lazy loading, so service will be started on system load.
             * This wouldnn't cause any harm in cause of our light-weight implementation (AccessTokenManager is small object)
             * But can be problems with big services, especcialy if they have own dependency.
             */

            //var tokenManger = new AccessTokenManager();

            //Bind<IAccessTokenGenerator>().ToConstant(tokenManger);
            //Bind<IAccessTokenValidator>().ToConstant(tokenManger);
        }
    }
}
