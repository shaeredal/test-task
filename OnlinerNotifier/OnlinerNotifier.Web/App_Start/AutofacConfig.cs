﻿using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OAuth2;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.DAL;
using OnlinerNotifier.Scheduler;
using OnlinerNotifier.Scheduler.Jobs;

namespace OnlinerNotifier
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            RegisterControllers(builder);
            RegisterApiControllers(builder);
            RegisterModelBinders(builder);
            RegisterModule(builder);
            RegisterSource(builder);
            RegisterFilterProvider(builder);
            RegisterTypes(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }

        private static void RegisterApiControllers(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        private static void RegisterModelBinders(ContainerBuilder builder)
        {
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
        }

        private static void RegisterModule(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacWebTypesModule>();
        }

        private static void RegisterSource(ContainerBuilder builder)
        {
            builder.RegisterSource(new ViewRegistrationSource());
        }

        private static void RegisterFilterProvider(ContainerBuilder builder)
        {
            builder.RegisterFilterProvider();
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationRoot>().AsSelf().SingleInstance();
            builder.RegisterType<UnitOfWork>().AsSelf().InstancePerDependency();
            RegisterServices(builder);
            RegisterMappers(builder);
            RegisterJobs(builder);
            builder.RegisterType<EmailValidator>().AsSelf();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerDependency();
            builder.RegisterType<OnlinerSearchService>().As<IOnlinerSearchService>().InstancePerDependency();
            builder.RegisterType<PricesCheckingService>().As<IPricesCheckingService>().InstancePerDependency();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerDependency();
            builder.RegisterType<EmailSendingService>().As<IEmailSendingService>().InstancePerDependency();
            builder.RegisterType<TrackingService>().As<ITrackingService>().InstancePerDependency();
        }

        private static void RegisterMappers(ContainerBuilder builder)
        {
            builder.RegisterType<UserMapper>().AsSelf().InstancePerDependency();
            builder.RegisterType<ProductMapper>().AsSelf().InstancePerDependency();
            builder.RegisterType<PriceChangesMapper>().AsSelf().InstancePerDependency();
            builder.RegisterType<UserProductsMapper>().AsSelf().InstancePerDependency();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterType<CheckPricesJob>().AsSelf().InstancePerDependency();
            builder.RegisterType<SetNotifiersJob>().AsSelf().InstancePerDependency();
        }
    }
}