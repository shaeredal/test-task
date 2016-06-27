using System;
using System.Web.Hosting;
using FluentScheduler;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class SetNotifiersJob : IJob, IRegisteredObject
    {
        private readonly object lockObject = new object();

        private bool shuttingDown;

        private readonly INotificationService notificationService;

        private readonly IEmailSendingService emailSendingService;

        public SetNotifiersJob(INotificationService notificationService, IEmailSendingService emailSendingService)
        {
            HostingEnvironment.RegisterObject(this);

            this.notificationService = notificationService;
            this.emailSendingService = emailSendingService;
        }

        public void Execute()
        {
            lock (lockObject)
            {
                if (shuttingDown)
                    return;

                var data = notificationService.GetNotificationData();
                foreach (var user in data)
                {
                    SetNotification(user);
                }
            }
        }

        private void SetNotification(NotificationDataModel user)
        {
            var notificationTime = GetNotificationTime(user.User.NotificationTime);
            JobManager.AddJob(() => emailSendingService.SendChanges(user.User, user.Products),
                (s) => s.ToRunOnceAt(notificationTime));
        }

        private DateTime GetNotificationTime(DateTime userNotificationTimeUtc)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            var userNotificationTime = userNotificationTimeUtc + offset;
            var notificationTime = DateTime.Today + userNotificationTime.TimeOfDay;
            if (notificationTime.TimeOfDay < DateTime.Now.TimeOfDay)
            {
                notificationTime += TimeSpan.FromDays(1);
            }
            return notificationTime;
        }

        public void Stop(bool immediate)
        {
            lock (lockObject)
            {
                shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}