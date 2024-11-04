using Quartz;
using Quartz.Spi;

namespace PassportApplication.Quartz
{
    /// <summary>
    /// Implements IHostedService
    /// </summary>
    public class QuartzHostedService : IHostedService
    {
        private ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IJobDetail _jobDetail;
        private readonly ITrigger _trigger;

        /// <summary>
        /// Constructor of QuartzHostedService
        /// </summary>
        /// <param name="schedulerFactory">Scheduler factory</param>
        /// <param name="jobFactory">Job factory</param>
        /// <param name="jobDetail">Job detail</param>
        /// <param name="trigger">Trigger</param>
        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, IJobDetail jobDetail, ITrigger trigger)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _jobDetail = jobDetail;
            _trigger = trigger;
        }

        /// <summary>
        /// Scheduler
        /// </summary>
        public IScheduler Scheduler { get; set; }

        /// <summary>
        /// Starts hosted service work
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            await Scheduler.Start();
            await Scheduler.ScheduleJob(_jobDetail, _trigger);
        }

        /// <summary>
        /// Stops hosted service work
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}
