using TimeTracker.API.Models;
using TimeTracker.API.Interfaces;

namespace TimeTracker.API.Services
{
    public class TimeTrackerService : ITimeTrackerService
    {
        private DateTime? _startTime;

        private readonly List<int> _sessionMinutes = new();

        public void Start()
        {
            if (_startTime is not null)
                throw new InvalidOperationException("Timer is already running.");

            _startTime = DateTime.UtcNow;
        }

        public TimeResult Stop()
        {
            if (_startTime is null)
                throw new InvalidOperationException("Timer has not been started.");

            var endTime = DateTime.UtcNow;
            var duration = endTime - _startTime.Value;
            var minutes = (int)duration.TotalMinutes;

            string comment = GenerateComment(minutes);

            _sessionMinutes.Add(minutes);

            _startTime = null;

            return new TimeResult
            {
                DurationMinutes = minutes,
                Comment = comment
            };
        }

        public (bool IsRunning, DateTime? StartedAt) GetStatus()
        {
            if (_startTime is null)
                return (false, null);

            return (true, _startTime);
        }

        public int GetTotalMinutes()
        {
            return _sessionMinutes.Sum();
        }

        public void Reset()
        {
            _startTime = null;        
            _sessionMinutes.Clear();          
        }

        private string GenerateComment(int minutes)
        {
            if (minutes < 20)
                return "Short warm-up session.";

            if (minutes < 50)
                return "Nice focus block! Keep going.";

            if (minutes < 90)
                return "Great deep work session!";

            return "Elite level concentration.";
        }
    }
}
