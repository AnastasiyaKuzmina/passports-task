using Quartz;
using Quartz.Spi;

namespace PassportApplication.Quartz.Jobs
{
    /// <summary>
    /// Implements IJobFactory
    /// </summary>
    public class UpdateDatabaseJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor of UpdateDatabaseJobFactory
        /// </summary>
        /// <param name="serviceProvider"></param>
        public UpdateDatabaseJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets new job
        /// </summary>
        /// <param name="bundle">TriggerFiredBundle instance</param>
        /// <param name="scheduler">IScheduler</param>
        /// <returns>IJob</returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService<UpdateDatabaseJob>();
        }

        /// <summary>
        /// Returns job
        /// </summary>
        /// <param name="job">Job</param>
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
