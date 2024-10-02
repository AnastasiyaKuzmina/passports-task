using PassportApplication.Models;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;
using System.Diagnostics;
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

        public Task Execute(IJobExecutionContext context)
        {
            _updateDatabaseService.UpdateDatabase(_options.FileUrl, _applicationContext);
            return Task.CompletedTask;
        }
    }
}
