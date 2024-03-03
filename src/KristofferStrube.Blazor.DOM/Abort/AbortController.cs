using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM.Abort;

/// <summary>
/// Though promises do not have a built-in aborting mechanism, many APIs using them require abort semantics.
/// <see cref="AbortController"/> is meant to support these requirements by providing an <see cref="AbortAsync(IJSObjectReference?)"/> method that toggles the state of a corresponding <see cref="AbortSignal{TAbortEvent}"/> object.
/// The API which wishes to support aborting can accept an <see cref="AbortSignal"/> object, and use its state to determine how to proceed.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#abortcontroller">See the API definition here</see></remarks>
[IJSWrapperConverter]
public class AbortController : BaseJSWrapper, IJSCreatable<AbortController>
{
    /// <inheritdoc/>
    public static async Task<AbortController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<AbortController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new AbortController(jSRuntime, jSReference, options));
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
        AbortController abortController = new(jSRuntime, jSInstance, new() { DisposesJSReference = true });
        return abortController;
    }

    /// <inheritdoc/>
    protected AbortController(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <summary>
    /// Returns the <see cref="AbortSignal"/> object associated with this object.
    /// </summary>
    /// <returns></returns>
    public async Task<AbortSignal> GetSignalAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "signal");
        return await AbortSignal.CreateAsync(JSRuntime, jSInstance, new() { DisposesJSReference = true });
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
