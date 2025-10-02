namespace SchedularDemo.Models.ServiceModels
{
    public class DailyJob
    {
        public string Name { get; set; } = string.Empty;
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public Func<Task> Action { get; set; } = default!;
    }

}
