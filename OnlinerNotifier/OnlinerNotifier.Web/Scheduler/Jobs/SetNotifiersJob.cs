using System.Web.Hosting;
using FluentScheduler;
using OnlinerNotifier.BLL.Services;

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
                    JobManager.AddJob(() => emailSendingService.SendChanges(user.User, user.Products), (s) => s.ToRunNow());//OnceAt(user.User.NotificationTime));
                }
            }
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