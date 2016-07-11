using System.Web.Hosting;
using FluentScheduler;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Interfaces;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class CheckPricesJob : IJob, IRegisteredObject
    {
        private readonly object lockObject = new object();

        private bool shuttingDown;

        private readonly IPricesCheckingService pricesCheckingService;

        public CheckPricesJob(IPricesCheckingService pricesCheckingService)
        {
            HostingEnvironment.RegisterObject(this);

            this.pricesCheckingService = pricesCheckingService;
        }

        public void Execute()
        {
            lock (lockObject)
            {
                if (shuttingDown)
                    return;

                pricesCheckingService.Check();
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