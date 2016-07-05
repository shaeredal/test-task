﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using FluentScheduler;
using OnlinerNotifier.App_Start;
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
            ToastNotificationsConfig.SetProviderName();
            AutofacConfig.RegisterDependencies();
            JobManager.JobFactory = new JobFactory(GlobalConfiguration.Configuration);
            JobManager.Initialize(new MyRegistry());
        }
    }
}