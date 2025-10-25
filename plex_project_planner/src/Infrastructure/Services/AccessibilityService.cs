using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace PlexProjectPlanner.Infrastructure.Services
{
    public interface IAccessibilityService
    {
        Task InitializeAsync();
        Task SetFocusAsync(string elementId);
        Task AnnounceAsync(string message);
        Task UpdateSkipLinksAsync();
        Task EnableHighContrastModeAsync();
        Task DisableHighContrastModeAsync();
    }

    public class AccessibilityService : IAccessibilityService
    {
        private readonly IJSRuntime _jsRuntime;

        public AccessibilityService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            // Initialize accessibility features
            await UpdateSkipLinksAsync();
        }

        public async Task SetFocusAsync(string elementId)
        {
            await _jsRuntime.InvokeVoidAsync("focusElement", elementId);
        }

        public async Task AnnounceAsync(string message)
        {
            await _jsRuntime.InvokeVoidAsync("announce", message);
        }

        public async Task UpdateSkipLinksAsync()
        {
            // Update skip navigation links
            await _jsRuntime.InvokeVoidAsync("updateSkipLinks");
        }

        public async Task EnableHighContrastModeAsync()
        {
            await _jsRuntime.InvokeVoidAsync("addClassToBody", "high-contrast");
        }

        public async Task DisableHighContrastModeAsync()
        {
            await _jsRuntime.InvokeVoidAsync("removeClassFromBody", "high-contrast");
        }
    }
}