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

        private readonly ITimeCalculationService notificationTimeCalculationService;

        public SetNotifiersJob(INotificationService notificationService, IEmailSendingService emailSendingService,
            ITimeCalculationService notificationTimeCalculationService)
        {
            HostingEnvironment.RegisterObject(this);

            this.notificationService = notificationService;
            this.emailSendingService = emailSendingService;
            this.notificationTimeCalculationService = notificationTimeCalculationService;
        }

        public void Execute()
        {
            lock (lockObject)
            {
                if (shuttingDown)
                    return;
                StartNotificationJobs();    
            }
        }

        private void StartNotificationJobs()
        {
            var data = notificationService.GetNotificationData();
            foreach (var user in data)
            {
                AddNotificationJob(user);
            }
        }

        private void AddNotificationJob(NotificationDataModel user)
        {
            var notificationTime = notificationTimeCalculationService.CalculateNotificationTime(user.User.NotificationTime);
            JobManager.AddJob(() => emailSendingService.SendChanges(user.User, user.Products),
                (s) => s.ToRunOnceAt(notificationTime));
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