using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject.Web.Common;
using WorkWithDB.DAL.Abstract;
using WorkWithDB.DAL.SqlServer.Infrastructure;
using WorkWithDB.DAL.SqlServer.Repository;

namespace WorkWithDB.DAL.SqlServer
{
    public class AdonetSqlServerDalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlogPostRepository>().To<BlogPostRepository>();
            Bind<IBlogUserRepository>().To<BlogUserRepository>();
            Bind<IAuthRepository>().To<AuthRepository>();
            Bind<ITransactionManager>().To<SqlTransactionManager>().InRequestScope();
            Bind<SqlConnection>().ToMethod(ctx => SqlConnectionFactory.CreateConnection()).InRequestScope();
        }
    }
}
