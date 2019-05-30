using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Extensions.Hosting;
using System.IO;
using Topshelf.Extensions.Hosting;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices((host, services) =>
                {
                    services.Configure<HelloConfig>(host.Configuration.GetSection("HelloConfig"));
                    services.AddTransient<HelloJob>();

                    services.AddQuartz(quartz =>
                    {
                        quartz.ScheduleJob(
                            JobBuilder
                                .Create(typeof(HelloJob))
                                .Build(),
                            TriggerBuilder
                                .Create()
                                .WithSimpleSchedule(s => s
                                    .WithIntervalInSeconds(5)
                                    .RepeatForever())
                                .Build());
                    });
                })
                .RunAsTopshelfService(host =>
                {
                    host.SetDescription("Hello Service");
                    host.SetDisplayName("Hello Service");
                    host.SetServiceName("HelloService");
                });
        }
    }
}