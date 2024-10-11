using PassportApplication.Models;
using PassportApplication.Services;
using PassportApplication.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Quartz;

namespace PassportApplication.Jobs
{
    public class UpdateDatabaseJob : IJob
    {
        private readonly IUpdateService _updateService;
        //private readonly UpdateDatabaseJobOptions _options;

        public UpdateDatabaseJob(IUpdateService updateService, ApplicationContext applicationContext)
        {
            _updateService = updateService;
            //_options = options.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("Here!");
            await _updateService.Update();
        }
    }
}
