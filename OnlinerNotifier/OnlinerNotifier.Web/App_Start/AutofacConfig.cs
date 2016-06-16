using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OAuth2;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();

            builder.RegisterType<AuthorizationRoot>().AsSelf().SingleInstance();

            builder.RegisterType<UnitOfWork>().AsSelf().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}