using Microsoft.EntityFrameworkCore;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using PassportApplication.Database;
using PassportApplication.Options;
using PassportApplication.Options.Enums;
using PassportApplication.Options.DatabaseOptions;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;
using PassportApplication.Services.CopyServices;
using PassportApplication.Quartz.Jobs;

using QHostedService = PassportApplication.Quartz.QuartzHostedService;

namespace PassportApplication.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddDatabase(this IServiceCollection services, Settings settings)
        {
            switch (settings.DatabaseMode)
            {
                case DatabaseMode.FileSystem:
                    services.AddSingleton(f => settings.DatabaseSettings);
                    services.AddSingleton<FileSystemDatabase>();
                    return;
                case DatabaseMode.PostgreSql:
                    if (settings.DatabaseSettings is PostgreSqlSettings ps)
                    {
                        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(ps.ConnectionString));
                        return;
                    }
                    throw new Exception();
                case DatabaseMode.MsSql:
                    if (settings.DatabaseSettings is MsSqlSettings ms)
                    {
                        services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(ms.ConnectionString));
                        return;
                    }
                    throw new Exception();
            }
        }

        public static void AddQuartzService(this IServiceCollection services, Settings settings)
        {
            var quartzServiceProvider = GetQuartzServiceProvider(settings);

            services.AddSingleton<IJobFactory>(f => new UpdateDatabaseJobFactory(quartzServiceProvider));
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddJob();
            services.AddTrigger();
            services.AddHostedService<QHostedService>();
        }

        private static ServiceProvider GetQuartzServiceProvider(Settings settings)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDatabase(settings);
            serviceCollection.AddSingleton(u => settings.UpdateSettings);
            serviceCollection.AddSingleton<UpdateDatabaseJob>();
            serviceCollection.AddSingleton<IFileDownloadService, FileDownloadService>();
            serviceCollection.AddCopy(settings);
            serviceCollection.AddSingleton<IUnpackService, UnpackService>();
            serviceCollection.AddSingleton<IUpdateService, UpdateService>();

            return serviceCollection.BuildServiceProvider();
        }

        private static void AddCopy(this IServiceCollection services, Settings settings)
        {
            switch (settings.DatabaseMode)
            {
                case DatabaseMode.FileSystem:
                    services.AddSingleton<ICopyService, FileSystemCopyService>();
                    return;
                case DatabaseMode.PostgreSql:
                    services.AddSingleton<ICopyService, PostgreSqlCopyService>();
                    return;
                case DatabaseMode.MsSql:
                    services.AddSingleton<ICopyService, SqlBulkCopyService>();
                    return;
            }
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
