using Quartz;
using Quartz.Spi;

namespace PassportApplication.Quartz.Jobs
{
    public class UpdateDatabaseJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateDatabaseJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService<UpdateDatabaseJob>();
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
