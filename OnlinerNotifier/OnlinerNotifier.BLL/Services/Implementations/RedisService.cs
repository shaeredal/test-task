﻿using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Redis;
using StackExchange.Redis;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class RedisService : IRedisService
    {
        private IDatabase redis;
        private ISubscriber sub;

        public RedisService()
        {
            redis = RedisConnector.Connection.GetDatabase();
            sub = RedisConnector.Connection.GetSubscriber();
        }

        public void PublishEmail(NotificationEmailModel emailModel)
        {
            var userId = emailModel.UserId;
            redis.StringSet($"notification email:{userId}:address", emailModel.EmailAddress);
            redis.StringSet($"notification email:{userId}:name", emailModel.ReceiverName);
            redis.StringSet($"notification email:{userId}:body", emailModel.EmailBody);
            redis.StringSet($"notification email:{userId}:time", emailModel.NotificationTime.Ticks);
            sub.Publish("notification email", userId);
        }

        public NotificationEmailModel GetEmail(int key)
        {
            return new NotificationEmailModel()
            {
                EmailAddress = redis.StringGet($"notification email:{key}:address"),
                ReceiverName = redis.StringGet($"notification email:{key}:name"),
                EmailBody = redis.StringGet($"notification email:{key}:body")
            };
        }
    }
}