using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;

using PassportApplication.Models;
using PassportApplication.Jobs;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;

namespace PassportApplication
{
    /// <summary>
    /// Application setup
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Constructor of Setup
        /// </summary>
        /// <param name="configuration">Builder configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Builder configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Services setup
        /// </summary>
        /// <param name="services"></param>
        public async void ConfigureServices(IServiceCollection services)
        {
            string? connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

            serviceCollection.AddOptions().Configure<UpdateDatabaseJobOptions>(opt =>
            {
                opt.FileUrl = Configuration["Download:FileUrl"];
            });

            serviceCollection.AddSingleton<UpdateDatabaseJob>();
            serviceCollection.AddSingleton<IUpdateDatabaseService, UpdateDatabaseService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            var job = JobBuilder.Create<UpdateDatabaseJob>()
                .WithIdentity("UpdateDatabaseJob", "Group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("UpdateDatabaseTrigger", "Group1")
                .StartNow()
                .WithSimpleSchedule(s => s
                    .WithIntervalInHours(24)
                    .RepeatForever())
                .Build();

            scheduler.JobFactory = new UpdateDatabaseJobFactory(serviceProvider);
            await scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Usage setup
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
