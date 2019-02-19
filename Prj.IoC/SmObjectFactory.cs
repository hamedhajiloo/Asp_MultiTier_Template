using System;
using System.Threading;
using StructureMap;
using Prj.IoC.Registries;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;
using Prj.Common.JsonWebToken;
using WebApi.OutputCache.Core.Cache;
using Prj.IocConfig.Registries;

namespace Prj.IoC
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {
                ioc.AddRegistry<AspNetIdentityRegistry>();
                ioc.AddRegistry<AutoMapperRegistry>();
                ioc.AddRegistry<TaskSchedulerRegistry>();

                ioc.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();
                    s.Assembly("Prj.Service");
                });
                
                ioc.For<IAppJwtConfiguration>().Singleton().Use(() => AppJwtConfiguration.Config);
                ioc.For<IOAuthAuthorizationServerProvider>().Singleton().Use<AppOAuthProvider>();
                ioc.For<IAuthenticationTokenProvider>().Singleton().Use<RefreshTokenProvider>();
                ioc.For<IApiOutputCache>().Singleton().Use<MemoryCacheDefault>();
            });

            
        }
    }
}
