using System;
using FluentScheduler;
using OnlinerNotifier.BLL.Redis;
using OnlinerNotifier.BLL.Services.EmailServices;
using OnlinerNotifier.BLL.Services.Interfaces;
using StackExchange.Redis;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class SendEmailJob : IJob
    {
        private object lockObject = new object();
             
        private IRedisService redisService;

        private IEmailModelSender _emailModelSender;

        private IDatabase redis;

        private ISubscriber sub;

        public SendEmailJob(IRedisService redisService, IEmailModelSender _emailModelSender)
        {
            this.redisService = redisService;
            this._emailModelSender = _emailModelSender;
            redis = RedisConnector.Connection.GetDatabase();
            sub = RedisConnector.Connection.GetSubscriber();
        }

        public void Execute()
        {
            sub.Subscribe("notification email", (channel, key) =>
            {
                SetJob((int)key);
            });
        }

        private void SetJob(int key)
        {
            var notificationTime = new DateTime((long)redis.StringGet($"notification email:{key}:time"));
            JobManager.AddJob(() =>
            {
                lock (lockObject)
                {
                    var email = redisService.GetEmail(key);
                    _emailModelSender.Send(email);
                }
            }, (s) => s.ToRunOnceAt(notificationTime));
        }
    }
}