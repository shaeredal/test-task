using System;
using StackExchange.Redis;

namespace OnlinerNotifier.BLL.Redis
{
    public class RedisConnectorHelper
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        static RedisConnectorHelper()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }
    }
}
