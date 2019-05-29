using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Quartz.Extensions.Hosting
{
    public class JobSchedulerService : IHostedService
    {
        private readonly IScheduler _scheduler;
        private readonly JobScheduleLibrary _library;

        public JobSchedulerService(IScheduler scheduler, JobScheduleLibrary library)
        {
            _scheduler = scheduler;
            _library = library;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach(var jobSchedule in _library.Jobs)
                await _scheduler.ScheduleJob(jobSchedule.Detail, jobSchedule.Trigger, cancellationToken);

            _scheduler.Start(cancellationToken).Wait();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}