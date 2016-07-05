using System.Configuration;
using NetMQ;
using NetMQ.WebSockets;
using NetMQ.zmq;
using OnlinerNotifier.Configuration;
using OnlinerNotifier.ToastNotifier;

namespace OnlinerNotifier.App_Start
{
    public class ToastNotificationsConfig
    {
        private static string providerName;

        public static string ProviderName => providerName;

        public static void SetProviderName()
        {
            var section = (ToastNotificationsConfiguration) ConfigurationManager.GetSection("toastNotifications");
            providerName = section.NotificationsProviderType;
        }
    }
}