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

        public SetNotifiersJob(INotificationService notificationService)
        {
            HostingEnvironment.RegisterObject(this);

            this.notificationService = notificationService;
        }

        public void Execute()
        {
            lock (lockObject)
            {
                if (shuttingDown)
                    return;

                var data = notificationService.GetNotificationData();
                //TODO: Start jobs that send emails
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