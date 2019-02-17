using System.Linq;
using StructureMap;
using AutoMapper;

namespace Prj.IoC.Registries
{
    public class AutoMapperRegistry : Registry
    {
        public AutoMapperRegistry()
        {
            this.Scan(scan =>
            {
                scan.TheCallingAssembly();
                //scan.AssemblyContainingType<SomeType>(); // for other asms, if any.
                scan.WithDefaultConventions();
                scan.Assembly("Prj.Web");
                scan.AddAllTypesOf<Profile>().NameBy(item => item.FullName);
            });

            this.For<MapperConfiguration>().Singleton().Use("MapperConfig", ctx =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMissingTypeMaps = true; // It will connect `Person` & `PersonViewModel` automatically.
                    AddAllCustomAutoMapperProfiles(ctx, cfg);
                });
                config.AssertConfigurationIsValid();

                return config;
            });

            this.For<IMapper>().Singleton().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance));
        }

        private static void AddAllCustomAutoMapperProfiles(IContext ctx, IMapperConfigurationExpression cfg)
        {
            var profiles = ctx.GetAllInstances<Profile>().ToList();
            foreach (var profile in profiles)
            {
                cfg.AddProfile(profile);
            }
        }
    }
}