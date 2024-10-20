using Quartz;
using Quartz.Spi;
using System.Diagnostics;

namespace PassportApplication.Quartz
{
    public class QuartzHostedService : IHostedService
    {
        private ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IJobDetail _jobDetail;
        private readonly ITrigger _trigger;

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IJobDetail jobDetail, ITrigger trigger)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobDetail = jobDetail;
            _trigger = trigger;
        }

        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            await Scheduler.Start();
            await Scheduler.ScheduleJob(_jobDetail, _trigger);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}
