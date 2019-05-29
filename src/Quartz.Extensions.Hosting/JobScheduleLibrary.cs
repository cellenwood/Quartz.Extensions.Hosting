using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Quartz.Extensions.Hosting
{
    public class JobScheduleLibrary
    {
        private readonly IServiceCollection _services;

        public List<JobSchedule> Jobs { get; }

        public JobScheduleLibrary(IServiceCollection services)
        {
            _services = services;
            Jobs = new List<JobSchedule>();
        }

        public void AddJob(JobSchedule schedule)
        {
            _services.AddTransient(schedule.Detail.JobType);
            Jobs.Add(schedule);
        }

        public void AddJob(ITrigger trigger, IJobDetail detail)
        {
            AddJob(new JobSchedule(trigger, detail));
        }
    }
}


