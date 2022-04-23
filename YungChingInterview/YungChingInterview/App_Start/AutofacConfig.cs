using System.Reflection;
using System.Web.Mvc;

using Service;

using Autofac;
using Autofac.Integration.Mvc;

using Repository;
using Service.InterFace;
using Repository.InterFace;

namespace YungChingInterview
{

    public class AutofacConfig
    {

        public static void Bootstrapper()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());



            builder.RegisterType<StoreService>().As<IStoreService>();
            builder.RegisterType<StoreRepository>().As<IStoreRepository>();



            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}