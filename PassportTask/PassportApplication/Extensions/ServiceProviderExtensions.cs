using Microsoft.EntityFrameworkCore;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using PassportApplication.Database;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;
using PassportApplication.Quartz.Jobs;

using QHostedService = PassportApplication.Quartz.QuartzHostedService;

namespace PassportApplication.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddQuartzService(this IServiceCollection services, string? connection, IConfiguration configuration)
        {
            var quartzServiceProvider = GetQuartzServiceProvider(connection, configuration);

            services.AddSingleton<IJobFactory>(f => new UpdateDatabaseJobFactory(quartzServiceProvider));
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddJob();
            services.AddTrigger();
            services.AddHostedService<QHostedService>();
        }

        private static ServiceProvider GetQuartzServiceProvider(string? connection, IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            serviceCollection.AddSingleton(c => configuration);
            serviceCollection.AddSingleton<UpdateDatabaseJob>();
            serviceCollection.AddSingleton<IFileDownloadService, FileDownloadService>();
            serviceCollection.AddSingleton<IDatabaseService, DatabaseService>();
            serviceCollection.AddSingleton<IUnpackService, UnpackService>();
            serviceCollection.AddSingleton<IUpdateService, UpdateService>();

            return serviceCollection.BuildServiceProvider();
        }

        private static void AddJob(this IServiceCollection services)
        {
            var job = JobBuilder.Create<UpdateDatabaseJob>()
                .WithIdentity("UpdateDatabaseJob", "Group1")
                .Build();

            services.AddSingleton(j => job);
        }

        private static void AddTrigger(this IServiceCollection services)
        {
            var trigger = TriggerBuilder.Create()
               .WithIdentity("UpdateDatabaseTrigger", "Group1")
               .StartNow()
               .WithSimpleSchedule(s => s
               .WithIntervalInHours(24)
               .RepeatForever())
               .Build();

            services.AddSingleton(t => trigger);
        }
    }
}
