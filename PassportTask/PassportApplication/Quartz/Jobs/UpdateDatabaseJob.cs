using Quartz;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Quartz.Jobs
{
    /// <summary>
    /// Update database job class
    /// </summary>
    public class UpdateDatabaseJob : IJob
    {
        private readonly IUpdateService _updateService;

        /// <summary>
        /// Constructor of UpdateDatabaseJob
        /// </summary>
        /// <param name="updateService"></param>
        public UpdateDatabaseJob(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        /// <summary>
        /// Execute job method
        /// </summary>
        /// <param name="context">IJobExecutionContext</param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            await _updateService.UpdateAsync();
        }
    }
}
