using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM.Extensions;

/// <summary>
/// Extension methods for getting helper tasks for JSInterop.
/// </summary>
public static class IJSRuntimeExtensions
{
    /// <summary>
    /// Gets a helper that enables asynchronous invocation of helper methods.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await GetHelperAsync<IJSObjectReference>(jSRuntime);
    }

    /// <summary>
    /// Gets a helper that enables synchronous invocation of helper methods.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public static async Task<IJSInProcessObjectReference> GetInProcessHelperAsync(this IJSRuntime jSRuntime)
    {
        return await GetHelperAsync<IJSInProcessObjectReference>(jSRuntime);
    }

    private static async Task<T> GetHelperAsync<T>(IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<T>("import", "./_content/KristofferStrube.Blazor.DOM/KristofferStrube.Blazor.DOM.js");
    }
}