using Microsoft.EntityFrameworkCore;
using PassportApplication.Database;
using PassportApplication.Jobs;
using PassportApplication.Services.Interfaces;
using PassportApplication.Services;
using System.Data;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace PassportApplication
{
    public static class ServiceProviderExtensions
    {
        public static void AddQuartzService(this IServiceCollection services, string? connection)
        {
            var quartzServiceProvider = GetQuartzServiceProvider(connection);

            services.AddSingleton<IJobFactory>(f => new UpdateDatabaseJobFactory(quartzServiceProvider));
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddJob();
            services.AddTrigger();
            services.AddHostedService<QuartzHostedService>();
        }

        private static ServiceProvider GetQuartzServiceProvider(string? connection)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            serviceCollection.AddSingleton<UpdateDatabaseJob>();
            serviceCollection.AddSingleton<IFileDownloadService, FileDownloadService>();
            serviceCollection.AddSingleton<IDatabaseService, DatabaseService>();
            serviceCollection.AddSingleton<IDataReader, ParserService>();
            serviceCollection.AddSingleton<IUnpackService, UnpackService>();
            serviceCollection.AddSingleton<IUpdateService, UpdateService>();

            return serviceCollection.BuildServiceProvider();
        }

        private static void AddJob(this IServiceCollection services)
        {
            var job = JobBuilder.Create<UpdateDatabaseJob>()
                .WithIdentity("UpdateDatabaseJob", "Group1")
                .Build();

            services.AddSingleton<IJobDetail>(j => job);
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

            services.AddSingleton<ITrigger>(t => trigger);
        }
    }
}
