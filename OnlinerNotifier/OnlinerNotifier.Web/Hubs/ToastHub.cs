using Microsoft.AspNet.SignalR;

namespace OnlinerNotifier.Hubs
{
    public class ToastHub : Hub
    {
        public void GetDeleteToast(string name)
        {
            Clients.Caller.showDeleteToast(name + " is deleted.");
        }

        public void GetAddToast(string name)
        {
            Clients.Caller.showAddToast(name + " is added.");
        }
    }
}