using PassportApplication.Models;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;
using Microsoft.Extensions.Options;
using Quartz;

namespace PassportApplication.Jobs
{
    public class UpdateDatabaseJob : IJob
    {
        private readonly IUpdateDatabaseService _updateDatabaseService;
        private readonly ApplicationContext _applicationContext;
        private readonly UpdateDatabaseJobOptions _options;

        public UpdateDatabaseJob(IUpdateDatabaseService updateDatabaseService, ApplicationContext applicationContext, IOptions<UpdateDatabaseJobOptions> options)
        {
            _updateDatabaseService = updateDatabaseService;
            _applicationContext = applicationContext;
            _options = options.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _updateDatabaseService.UpdateDatabase(_options.FileUrl, _applicationContext);
        }
    }
}
