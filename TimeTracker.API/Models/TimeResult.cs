namespace TimeTracker.API.Models
{
    public class TimeResult
    {
        public int DurationMinutes { get; set; }
        public string Comment { get; set; } = null!;
    }
}
