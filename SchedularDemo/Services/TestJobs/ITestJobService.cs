namespace SchedularDemo.Services.TestJobs
{
    public interface ITestJobService
    {
        Task RunEmailJobAsync();
        Task RunReportJobAsync();
        Task RunBackupJobAsync();
    }
}
