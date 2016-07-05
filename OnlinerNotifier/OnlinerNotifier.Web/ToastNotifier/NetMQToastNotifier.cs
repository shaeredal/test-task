using System.Net.Http;
using System.Text;
using NetMQ;
using NetMQ.WebSockets;

namespace OnlinerNotifier.ToastNotifier
{
    public class NetMQToastNotifier : IToastNotifier
    {
        private WSPublisher publisher;

        public NetMQToastNotifier(WSPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Send(HttpRequestMessage request, string message)
        {
            publisher.SendMore("toast").Send(message);
        }
    }
}