﻿using Quartz;
using PassportApplication.Services.Interfaces;

namespace PassportApplication.Quartz.Jobs
{
    public class UpdateDatabaseJob : IJob
    {
        private readonly IUpdateService _updateService;

        public UpdateDatabaseJob(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _updateService.UpdateAsync();
        }
    }
}
