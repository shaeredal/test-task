using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using NetMQ.WebSockets;

namespace OnlinerNotifier.NetMQSockets
{
    public class ToastSocket
    {
        public static void Bind(HttpConfiguration configuration)
        {
            var publisher = configuration.DependencyResolver.GetRootLifetimeScope().Resolve<WSPublisher>();
            publisher.Bind("ws://localhost:81");
        }
    }
}