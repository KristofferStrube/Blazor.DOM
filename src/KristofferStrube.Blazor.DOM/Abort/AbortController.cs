using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM.Abort;

/// <summary>
/// Though promises do not have a built-in aborting mechanism, many APIs using them require abort semantics.
/// <see cref="AbortController"/> is meant to support these requirements by providing an <see cref="AbortAsync(IJSObjectReference?)"/> method that toggles the state of a corresponding <see cref="AbortSignal{TAbortEvent}"/> object.
/// The API which wishes to support aborting can accept an <see cref="AbortSignal{TAbortEvent}"/> object, and use its state to determine how to proceed.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#abortcontroller">See the API definition here</see></remarks>
internal class AbortController : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AbortController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AbortController"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AbortController"/>.</returns>
    public static Task<AbortController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult<AbortController>(new(jSRuntime, jSReference));
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

    public async Task<TAbortEvent> GetSignalAsync<TAbortEvent>() where TAbortEvent : Event, IJSCreatable<TAbortEvent>
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "signal");
        return await TAbortEvent.CreateAsync(JSRuntime, jSInstance);
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
