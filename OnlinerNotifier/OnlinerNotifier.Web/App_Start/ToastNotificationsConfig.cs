using System.Configuration;
using OnlinerNotifier.Configuration;
using OnlinerNotifier.Enums;

namespace OnlinerNotifier.App_Start
{
    public class ToastNotificationsConfig
    {
        private static ToastNotificationProviderType providerType;

        public static ToastNotificationProviderType ProviderType => providerType;

        public static void SetProviderName()
        {
            var section = (ToastNotificationsConfiguration) ConfigurationManager.GetSection("toastNotifications");
            switch (section.NotificationsProviderType)
            {
                case "SignalR":
                    providerType = ToastNotificationProviderType.Signalr;
                    break;
                case "NetMQ":
                    providerType = ToastNotificationProviderType.NetMQ;
                    break;
                default:
                    throw new ConfigurationErrorsException();
            }
        }
    }
}