using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM.Extensions;

internal static class IJSRuntimeExtensions
{
    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await GetHelperAsync<IJSObjectReference>(jSRuntime);
    }

    internal static async Task<IJSInProcessObjectReference> GetInProcessHelperAsync(this IJSRuntime jSRuntime)
    {
        return await GetHelperAsync<IJSInProcessObjectReference>(jSRuntime);
    }

    private static async Task<T> GetHelperAsync<T>(IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<T>("import", "./_content/KristofferStrube.Blazor.DOM/KristofferStrube.Blazor.DOM.js");
    }
}