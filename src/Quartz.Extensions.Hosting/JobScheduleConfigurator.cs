using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace Quartz.Extensions.Hosting
{

    public static class JobScheduleConfigurator
    {
        public static IServiceCollection AddQuartz(this IServiceCollection services, Action<JobScheduleLibrary> libraryConfig)
        {
            services.AddHostedService<JobSchedulerService>();
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton(s => 
            {
                var library = new JobScheduleLibrary(services);
                libraryConfig(library);
                return library;
            });

            services.AddSingleton(s =>
            {
                var scheduler = s.GetRequiredService<ISchedulerFactory>().GetScheduler().Result;
                scheduler.JobFactory = s.GetRequiredService<IJobFactory>();
                return scheduler;
            });

            return services;
        }
    }
}