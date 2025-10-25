using System.Threading.Tasks;

namespace PlexProjectPlanner.Core.Interfaces
{
    public interface ILoggingService
    {
        Task LogInfoAsync(string message, string category = "General");
        Task LogWarningAsync(string message, string category = "General");
        Task LogErrorAsync(string message, string category = "General", System.Exception exception = null);
        Task LogDebugAsync(string message, string category = "General");
    }
}