using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Quartz.Extensions.Hosting
{
    public class JobSchedulerService : IHostedService
    {
        private readonly IScheduler _scheduler;

        public JobSchedulerService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler.Start(cancellationToken).Wait();
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}