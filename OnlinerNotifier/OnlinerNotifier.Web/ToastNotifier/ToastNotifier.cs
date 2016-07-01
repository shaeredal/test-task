using Microsoft.AspNet.SignalR;
using OnlinerNotifier.Hubs;

namespace OnlinerNotifier.ToastNotifier
{
    public class ToastNotifier : IToastNotifier
    {
        public void Send(string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ToastHub>();
            hubContext.Clients.All.notify(message); //temporary, have to be User(userId)
        }
    }
}