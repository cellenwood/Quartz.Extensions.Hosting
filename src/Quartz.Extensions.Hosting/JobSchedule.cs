using Quartz;

namespace Quartz.Extensions.Hosting
{
    public class JobSchedule
    {
        public ITrigger Trigger { get; private set; }
        public IJobDetail Detail { get; private set; }

        public JobSchedule(ITrigger trigger, IJobDetail detail)
        {
            Trigger = trigger;
            Detail = detail;
        }
    }
}