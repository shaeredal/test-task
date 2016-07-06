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

        private readonly IEmailService emailService;

        private readonly ITimeCalculationService notificationTimeCalculationService;

        public SetNotifiersJob(INotificationService notificationService, IEmailService emailService,
            ITimeCalculationService notificationTimeCalculationService)
        {
            HostingEnvironment.RegisterObject(this);

            this.notificationService = notificationService;
            this.emailService = emailService;
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
            foreach (var userData in data)
            {
                AddNotificationJob(userData);
            }
        }

        private void AddNotificationJob(NotificationDataModel userData)
        {
            var notificationTime = notificationTimeCalculationService.CalculateNotificationTime(userData.User.NotificationTime);
            JobManager.AddJob(() => emailService.SendChanges(userData.User, userData.Products),
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