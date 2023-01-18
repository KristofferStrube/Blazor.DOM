using KristofferStrube.Blazor.DOM.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM.Abort;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#abortcontroller">AbortController browser specs</see>
/// </summary>
internal class AbortController : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AbortController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AbortController"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AbortController"/>.</returns>
    public static AbortController Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        AbortController abortController = new(jSRuntime, jSReference);
        return abortController;
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A wrapper instance for a <see cref="AbortController"/>.</returns>
    public static async Task<AbortController> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructAbortController");
        AbortController abortController = new(jSRuntime, jSInstance);
        return abortController;
    }

    public AbortController(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    public async Task<AbortSignal> GetSignalAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "signal");
        return AbortSignal.Create(jSRuntime, jSInstance);
    }

    public async Task AbortAsync(string? reason = null)
    {
        await JSReference.InvokeVoidAsync("abort", reason);
    }

    public async Task AbortAsync(IJSObjectReference? reason = null)
    {
        await JSReference.InvokeVoidAsync("abort", reason);
    }
}
