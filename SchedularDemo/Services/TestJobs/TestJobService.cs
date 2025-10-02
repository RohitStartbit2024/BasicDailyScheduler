namespace SchedularDemo.Services.TestJobs
{
    public class TestJobService : ITestJobService
    {
        private readonly ILogger<TestJobService> _logger;

        public TestJobService(ILogger<TestJobService> logger)
        {
            _logger = logger;
        }

        public Task RunEmailJobAsync()
        {
            _logger.LogInformation("Email Job executed at {time}", DateTime.Now);
            return Task.CompletedTask;
        }

        public Task RunReportJobAsync()
        {
            _logger.LogInformation("Report Job executed at {time}", DateTime.Now);
            return Task.CompletedTask;
        }

        public Task RunBackupJobAsync()
        {
            _logger.LogInformation("Backup Job executed at {time}", DateTime.Now);
            return Task.CompletedTask;
        }
    }

}
