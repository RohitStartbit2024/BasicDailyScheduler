using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchedularDemo.Models.ServiceModels;
using SchedularDemo.Services.TestJobs;

namespace SchedularDemo.Services
{
    public class MultiDailyTaskService : BackgroundService
    {
        private readonly ITestJobService _jobService;
        private readonly TimeZoneInfo _indiaTimeZone;
        private readonly List<DailyJob> _jobs = new();

        public MultiDailyTaskService(ITestJobService jobService)
        {
            _jobService = jobService;
            _indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            // Register jobs
            _jobs.Add(new DailyJob
            {
                Name = "Email Job",
                Hour = 16,
                Minute = 13,
                Second = 00,
                Action = () => _jobService.RunEmailJobAsync()
            });

            _jobs.Add(new DailyJob
            {
                Name = "Report Job",
                Hour = 16,
                Minute = 14,
                Second = 00,
                Action = () => _jobService.RunReportJobAsync()
            });

            _jobs.Add(new DailyJob
            {
                Name = "Backup Job",
                Hour = 16,
                Minute = 15,
                Second = 00,
                Action = () => _jobService.RunBackupJobAsync()
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _indiaTimeZone);
                var upcomingJobs = _jobs.Select(job =>
                {
                    var targetTime = new DateTime(now.Year, now.Month, now.Day, job.Hour, job.Minute, job.Second);
                    if (now > targetTime)
                        targetTime = targetTime.AddDays(1);

                    return new { job, targetTime };
                }).OrderBy(x => x.targetTime);

                foreach (var scheduled in upcomingJobs)
                {
                    var delay = scheduled.targetTime - now;

                    try
                    {
                        await Task.Delay(delay, stoppingToken);
                        if (!stoppingToken.IsCancellationRequested)
                        {
                            await scheduled.job.Action();
                        }
                    }
                    catch (TaskCanceledException)
                    {
                        return;
                    }
                    now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _indiaTimeZone); // update for next job
                }
            }
        }
    }
}
