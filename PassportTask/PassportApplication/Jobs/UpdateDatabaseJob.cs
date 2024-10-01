using PassportApplication.Models;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;
using Quartz;

namespace PassportApplication.Jobs
{
    public class UpdateDatabaseJob : IJob
    {
        private readonly IUpdateDatabaseService _updateDatabaseService;
        private readonly UpdateDatabaseJobOptions _options;

        public UpdateDatabaseJob(IUpdateDatabaseService updateDatabaseService, UpdateDatabaseJobOptions options)
        {
            _updateDatabaseService = updateDatabaseService;
            _options = options;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _updateDatabaseService.UpdateDatabase(_options.FileUrl);
            return Task.CompletedTask;
        }
    }
}
