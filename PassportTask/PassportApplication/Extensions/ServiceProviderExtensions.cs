using Microsoft.EntityFrameworkCore;

using Quartz;
using Quartz.Impl;
using Quartz.Spi;

using PassportApplication.Database;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;
using PassportApplication.Services.CopyServices;
using PassportApplication.Quartz.Jobs;

using QHostedService = PassportApplication.Quartz.QuartzHostedService;

namespace PassportApplication.Extensions
{
    public static class ServiceProviderExtensions
    {
        const string firstUpdateMode = "SqlBulkCopy";
        const string secondUpdateMode = "PostgreSqlCopy";
        const string thirdUpdateMode = "FileSystem";

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string updateMode = configuration.GetSection("Database").GetSection("UpdateMode").Value ?? "";

            if (updateMode == firstUpdateMode)
            {
                string? sqlConnection = configuration.GetConnectionString("SqlConnection");
                services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(sqlConnection));
                return;
            }

            if (updateMode == secondUpdateMode)
            {
                string? NpgConnection = configuration.GetConnectionString("NpgSqlConnection");
                services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(NpgConnection));
                return;
            }

            if (updateMode == thirdUpdateMode)
            {
                return;
            }

            throw new NotImplementedException();
        }

        public static void AddQuartzService(this IServiceCollection services, IConfiguration configuration)
        {
            var quartzServiceProvider = GetQuartzServiceProvider(configuration);

            services.AddSingleton<IJobFactory>(f => new UpdateDatabaseJobFactory(quartzServiceProvider));
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddJob();
            services.AddTrigger();
            services.AddHostedService<QHostedService>();
        }

        private static ServiceProvider GetQuartzServiceProvider(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDatabase(configuration);
            serviceCollection.AddSingleton(c => configuration);
            serviceCollection.AddSingleton<UpdateDatabaseJob>();
            serviceCollection.AddSingleton<IFileDownloadService, FileDownloadService>();
            serviceCollection.AddCopy(configuration);
            serviceCollection.AddSingleton<IUnpackService, UnpackService>();
            serviceCollection.AddSingleton<IUpdateService, UpdateService>();

            return serviceCollection.BuildServiceProvider();
        }

        private static void AddCopy(this IServiceCollection services, IConfiguration configuration)
        {
            string updateMode = configuration.GetSection("Database").GetSection("UpdateMode").Value ?? "";

            if (updateMode == firstUpdateMode)
            {
                services.AddSingleton<ICopyService, SqlBulkCopyService>();
                return;
            }

            if (updateMode == secondUpdateMode)
            {
                services.AddSingleton<ICopyService, PostgreSqlCopyService>();
                return;
            }

            if (updateMode == thirdUpdateMode)
            {
                services.AddSingleton<ICopyService, FileSystemCopyService>();
                return;
            }

            throw new NotImplementedException();
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
