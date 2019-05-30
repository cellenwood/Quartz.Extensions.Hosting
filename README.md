# Quartz Hosting
Do you want to write services the dotnet core way, using the [IHostedService](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostedservice?view=aspnetcore-2.1) interface? Is Quartz server too much for your current need? Then use this extension to run your applicaton with quartz.

## Install
Quartz.Extensions.Hosting is available as a [Nuget-package](https://www.nuget.org/packages/Quartz.Extensions.Hosting). From the Package Manager Console enter:

        Install-Package Quartz.Extensions.Hosting

## How to use in a console application
Build a generic host the normal way. Use the [HostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.hostbuilder?view=aspnetcore-2.1) class as you normally do when building a console app. Then add in quartz and any scheduler configuration.

```csharp
    var builder = new HostBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddQuartz(scheduler => 
            {
                scheduler.ScheduleJob(
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
        });
```
				
## How to use in an MVC application

In Startup.cs, use the extension method in ConfigureServices. Order is not important with registration.

```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        services.AddQuartz(scheduler =>
        {
            scheduler.ScheduleJob(
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
    }
```