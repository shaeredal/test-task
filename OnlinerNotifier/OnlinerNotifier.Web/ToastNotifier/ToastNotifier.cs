using Microsoft.AspNet.SignalR;
using OnlinerNotifier.Hubs;

namespace OnlinerNotifier.ToastNotifier
{
    public class ToastNotifier : IToastNotifier
    {
        public void Send(string connectionId, string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ToastHub>();
            hubContext.Clients.Client(connectionId).notify(message);
        }
    }
}