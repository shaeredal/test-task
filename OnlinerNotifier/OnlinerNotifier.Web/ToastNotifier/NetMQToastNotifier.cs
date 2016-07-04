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
            //using (var context = NetMQContext.Create())
            //{
            //    using (WSPublisher pub = context.CreateWSPublisher())
            //    {
            //        pub.Bind("ws://localhost:81");

            //        Poller poller = new Poller();

            //        poller.Start();
            //        publisher = pub;
            //    }
            //}
        }

        public void Send(HttpRequestMessage request, string message)
        {
            publisher.SendMore("toast").Send(Encoding.ASCII.GetBytes(message));
        }
    }
}