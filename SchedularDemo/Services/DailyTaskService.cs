using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SchedularDemo.Services
{
    public class DailyTaskService : BackgroundService
    {
        private readonly ILogger<DailyTaskService> _logger;
        private readonly TimeZoneInfo _indiaTimeZone;

        public DailyTaskService(ILogger<DailyTaskService> logger)
        {
            _logger = logger;
            _indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            // On Linux/macOS, use "Asia/Kolkata"
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _indiaTimeZone);

                // Target time: 3:00 PM IST today
                var targetTime = new DateTime(now.Year, now.Month, now.Day, 14, 38, 0);

                if (now > targetTime)
                {
                    // If time already passed, schedule for tomorrow
                    targetTime = targetTime.AddDays(1);
                }

                var delay = targetTime - now;

                _logger.LogInformation("Next run scheduled at {time}", targetTime);

                // Wait until 3 PM IST
                await Task.Delay(delay, stoppingToken);

                if (!stoppingToken.IsCancellationRequested)
                {
                    RunScheduledJob();
                }
            }
        }

        private void RunScheduledJob()
        {
            _logger.LogInformation("Test Successful - job ran at {time}",
                TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _indiaTimeZone));
        }
    }
}
