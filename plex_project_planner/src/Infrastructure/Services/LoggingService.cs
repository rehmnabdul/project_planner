using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PlexProjectPlanner.Core.Interfaces;

namespace PlexProjectPlanner.Infrastructure.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public async Task LogInfoAsync(string message, string category = "General")
        {
            await Task.Run(() => _logger.LogInformation("[{Category}] {Message}", category, message));
        }

        public async Task LogWarningAsync(string message, string category = "General")
        {
            await Task.Run(() => _logger.LogWarning("[{Category}] {Message}", category, message));
        }

        public async Task LogErrorAsync(string message, string category = "General", System.Exception exception = null)
        {
            if (exception != null)
            {
                await Task.Run(() => _logger.LogError(exception, "[{Category}] {Message}", category, message));
            }
            else
            {
                await Task.Run(() => _logger.LogError("[{Category}] {Message}", category, message));
            }
        }

        public async Task LogDebugAsync(string message, string category = "General")
        {
            await Task.Run(() => _logger.LogDebug("[{Category}] {Message}", category, message));
        }
    }
}