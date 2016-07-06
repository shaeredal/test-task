using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using NetMQ;
using NetMQ.WebSockets;
using OAuth2;
using OnlinerNotifier.App_Start;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Redis;
using OnlinerNotifier.BLL.Templates.TemplatePathProvider;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL.Wrappers;
using OnlinerNotifier.DAL;
using OnlinerNotifier.Scheduler.Jobs;
using OnlinerNotifier.ToastNotifier;
using OnlinerNotifier.Enums;

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
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            RegisterServices(builder);
            RegisterMappers(builder);
            RegisterJobs(builder);
            RegisterToastNotificator(builder);
            builder.RegisterType<EmailValidator>().AsSelf();
            builder.RegisterType<SmtpClientWrapper>().As<ISmtpClient>();
            builder.RegisterType<TemplatePathProvider>().As<ITemplatePathProvider>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerDependency();
            builder.RegisterType<OnlinerSearchService>().As<IOnlinerSearchService>().InstancePerDependency();
            builder.RegisterType<PricesCheckingService>().As<IPricesCheckingService>().InstancePerDependency();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerDependency();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerDependency();
            builder.RegisterType<TrackingService>().As<ITrackingService>().InstancePerDependency();
            builder.RegisterType<TimeCalculationService>().As<ITimeCalculationService>().InstancePerDependency();
            builder.RegisterType<RedisService>().As<IRedisService>().InstancePerDependency();
        }

        private static void RegisterMappers(ContainerBuilder builder)
        {
            builder.RegisterType<UserMapper>().AsSelf().SingleInstance();
            builder.RegisterType<ProductMapper>().AsSelf().SingleInstance();
            builder.RegisterType<PriceChangesMapper>().AsSelf().SingleInstance();
            builder.RegisterType<UserProductsMapper>().AsSelf().SingleInstance();
            builder.RegisterType<EmailMapper>().AsSelf().SingleInstance();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterType<CheckPricesJob>().AsSelf().InstancePerDependency();
            builder.RegisterType<SetNotifiersJob>().AsSelf().InstancePerDependency();
        }

        private static void RegisterToastNotificator(ContainerBuilder builder)
        {
            switch (ToastNotificationsConfig.ProviderType)
            {
                case ToastNotificationProviderType.SignalR:
                    builder.RegisterType<SignalRToastNotifier>().As<IToastNotifier>();
                    break;
                case ToastNotificationProviderType.NetMQ:
                    builder.RegisterInstance(NetMQContext.Create().CreateWSPublisher()).SingleInstance();
                    builder.RegisterType<NetMQToastNotifier>().As<IToastNotifier>();
                    break;
            }
        }
    }
}