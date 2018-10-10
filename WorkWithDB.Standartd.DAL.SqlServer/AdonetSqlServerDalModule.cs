using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Text;
using Ninject.Modules;
using Ninject.Web.Common;
using WorkWithDB.DAL.Standard.Abstract;
using WorkWithDB.Standartd.DAL.SqlServer.Infrastructure;
using WorkWithDB.Standartd.DAL.SqlServer.Repository;

namespace WorkWithDB.Standartd.DAL.SqlServer
{
    public class AdonetSqlServerDalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlogPostRepository>().To<BlogPostRepository>();
            Bind<IBlogUserRepository>().To<BlogUserRepository>();
            Bind<IAuthRepository>().To<AuthRepository>();
            Bind<ITransactionManager>().To<SqlTransactionManager>().InRequestScope();
            Bind<SqlConnection>().ToMethod(ctx => SqlConnectionFactory.CreateConnection("")).InRequestScope();
        }
    }
}
