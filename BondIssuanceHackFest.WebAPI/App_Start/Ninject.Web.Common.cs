[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BondIssuanceHackFest.WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BondIssuanceHackFest.WebAPI.App_Start.NinjectWebCommon), "Stop")]


namespace BondIssuanceHackFest.WebAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using BondIssuanceHackFest.WebAPI.BondIssuance.Interfaces;
    using BondIssuanceHackFest.WebAPI.Models;
    using Ninject.Web.Common.WebHost;
    using BondIssuanceHackFest.DLL.DataModels;
    using System.Data.Entity;
    using BondIssuanceHackFest.DLL.IRepositories;
    using BondIssuanceHackFest.DLL.Repositories;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
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
                //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
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
            kernel.Bind<IBondRepository>().To<BondRepository>();
            kernel.Bind<IQuorumUserRepository>().To<QuorumUserRepository>();
            kernel.Bind<IQuorumNodeRepository>().To<QuorumNodeRepository>();
            kernel.Bind<IContractRepository>().To<ContractRepository>();
            kernel.Bind<System.Data.Entity.DbContext>().To<SqlContext>();
           
            //kernel.Bind<IBondRepository>().To/*<*/QuorumUserRepository>();


        }
    }
}