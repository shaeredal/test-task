using FluentScheduler;

namespace OnlinerNotifier.Scheduler
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Schedule<CheckPricesJob>().ToRunNow().AndEvery(1).Days();
        }
    }
}