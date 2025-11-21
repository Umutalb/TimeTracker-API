using TimeTracker.API.Models;

namespace TimeTracker.API.Interfaces
{
    public interface ITimeTrackerService
    {
        void Start();
        TimeResult Stop();
        (bool IsRunning, DateTime? StartedAt) GetStatus();
        int GetTotalMinutes();
        List<int> GetAllSessions();
    }
}
