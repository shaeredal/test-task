using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using Autofac;
using Autofac.Integration.WebApi;
using FluentScheduler;
using NetMQ.WebSockets;
using OnlinerNotifier.NetMQSockets;
using OnlinerNotifier.Scheduler;

namespace OnlinerNotifier
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);           
            AutofacConfig.RegisterDependencies();
            JobManager.JobFactory = new JobFactory(GlobalConfiguration.Configuration);
            JobManager.Initialize(new MyRegistry());

            Task.Run(() => new ToastSocket(GlobalConfiguration.Configuration.DependencyResolver.GetRootLifetimeScope().Resolve<WSPublisher>()).RegisterSocket());
        }
    }
}