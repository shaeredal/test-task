using NetMQ;
using NetMQ.WebSockets;

namespace OnlinerNotifier.NetMQSockets
{
    public class ToastSocket
    {
        private WSPublisher publisher;

        public ToastSocket(WSPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void RegisterSocket()
        {
            publisher.Bind("ws://localhost:81");
            Poller poller = new Poller();
            poller.Start();
        }
    }
}