using Autofac;
using BestPractices.Core.Repositories;
using BestPractices.Core.Services;
using BestPractices.Repository.Repositories;
using BestPractices.Service.Mapping;
using BestPractices.Service.Services;
using Module = Autofac.Module;
using System.Reflection;
using BestPractices.Repository.UnitOfWorks;
using BestPractices.Core.UnitOfWorks;
using BEstPractices.Repository;
using BestPractices.Caching;

namespace BestPractices.Api.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly=Assembly.GetExecutingAssembly();
            var RepoAssembly = Assembly.GetAssembly(typeof(AppDbContext));  
            var ServiceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            //InstancePerLifeTimeScope => Scope
            //InstancePerDepency => transient

            builder.RegisterAssemblyTypes(apiAssembly, RepoAssembly, ServiceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, RepoAssembly, ServiceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
            //InstancePerMatchingLifetimeScope  hataya sebep oluyor?


            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>(); //IproductService ınterfacesını gorunce ProductSertivceWithCachging'i al dedik.
            //InstancePerLifetimeScope => scope
            //InstancePerDepency => transient
        }
    }
}
