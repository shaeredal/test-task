using System.Web.Http;
using Microsoft.Owin;
using OnlinerNotifier.Enums;
using OnlinerNotifier.NetMQSockets;
using Owin;

[assembly: OwinStartup(typeof(OnlinerNotifier.Startup))]

namespace OnlinerNotifier
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            switch (ToastNotificationsConfig.ProviderType)
            {
                case ToastNotificationProviderType.SignalR:
                    app.MapSignalR();
                    break;
                case ToastNotificationProviderType.NetMQ:
                    ToastSocket.Bind(GlobalConfiguration.Configuration);
                    break;
            }
        }
    }
}
