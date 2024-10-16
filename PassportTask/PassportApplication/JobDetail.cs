using Quartz;

namespace PassportApplication
{
    public class JobDetail : IJobDetail
    {
        JobKey IJobDetail.Key => throw new NotImplementedException();

        string? IJobDetail.Description => throw new NotImplementedException();

        Type IJobDetail.JobType => throw new NotImplementedException();

        JobDataMap IJobDetail.JobDataMap => throw new NotImplementedException();

        bool IJobDetail.Durable => throw new NotImplementedException();

        bool IJobDetail.PersistJobDataAfterExecution => throw new NotImplementedException();

        bool IJobDetail.ConcurrentExecutionDisallowed => throw new NotImplementedException();

        bool IJobDetail.RequestsRecovery => throw new NotImplementedException();

        IJobDetail IJobDetail.Clone()
        {
            throw new NotImplementedException();
        }

        JobBuilder IJobDetail.GetJobBuilder()
        {
            throw new NotImplementedException();
        }
    }
}
