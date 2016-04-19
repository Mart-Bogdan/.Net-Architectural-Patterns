using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Services;
using System.Web.Mvc;
using Ninject.Parameters;
using Ninject.Syntax;
using WebApp.BL;
using WorkWithDB.DAL.SqlServer;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebApp.App_Start.NinjectWebCommon), "Stop")]

namespace WebApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
           // DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(bootstrapper.Kernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new AdonetSqlServerDalModule());
            kernel.Load(new BussinessLogicModule());
        }        
    }


    /// <summary>
    /// Dependency resolver implementation for ninject.
    /// </summary>
    public class NinjectDependencyResolver :System.Web.Http.Dependencies.IDependencyResolver
    {
        /// <summary>
        /// The resolution root used to resolve dependencies.
        /// </summary>
        private readonly IResolutionRoot resolutionRoot;

        private List<object> _services = new List<object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Ninject.Web.WebApi.NinjectDependencyResolver"/> class.
        /// </summary>
        /// <param name="resolutionRoot">The resolution root.</param>
        public NinjectDependencyResolver(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        /// <summary>
        /// Gets the service of the specified type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>The service instance or <see langword="null"/> if none is configured.</returns>
        public object GetService(Type serviceType)
        {
            var request = this.resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);

            var service = this.resolutionRoot.Resolve(request).SingleOrDefault();
            if (service != null)
            {
                lock (((ICollection)_services).SyncRoot)
                    _services.Add(service);
            }

            return service;
        }

        /// <summary>
        /// Gets the services of the specifies type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>All service instances or an empty enumerable if none is configured.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.resolutionRoot.GetAll(serviceType).ToList();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyResolver(resolutionRoot);
        }

        public void Dispose()
        {
            lock (((ICollection) _services).SyncRoot)
            {
                foreach (var service in _services)
                {
                    resolutionRoot.Release(service);
                }

                _services.Clear();
            }
        }
    }
}
