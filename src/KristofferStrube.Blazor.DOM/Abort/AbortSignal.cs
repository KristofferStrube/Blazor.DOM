using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// An object that holds a reason for why some action was aborted.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#abortsignal">See the API definition here</see></remarks>
public class AbortSignal<TAbortEvent> : EventTarget where TAbortEvent : Event, IJSCreatable<TAbortEvent>
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AbortSignal{TAbortEvent}"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AbortSignal{TAbortEvent}"/>.</param>
    /// <returns>A wrapper instance for a <see cref="AbortSignal{TAbortEvent}"/>.</returns>
    public static new Task<AbortSignal<TAbortEvent>> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult<AbortSignal<TAbortEvent>>(new(jSRuntime, jSReference));
    }

    /// <summary>
    /// Returns an AbortSignal instance whose abort reason is set to reason if not undefined; otherwise to an "AbortError" DOMException.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="reason">The associated abort reason, which is a JavaScript value.</param>
    /// <returns>A wrapper instance for a <see cref="AbortSignal{TAbortEvent}"/>.</returns>
    public static async Task<AbortSignal<TAbortEvent>> Abort(IJSRuntime jSRuntime, string? reason)
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("AbortSignal.abort", reason);
        return new AbortSignal<TAbortEvent>(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Returns an AbortSignal instance whose abort reason is set to reason if not undefined; otherwise to an "AbortError" DOMException.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="reason">The associated abort reason, which is a JavaScript value.</param>
    /// <returns>A wrapper instance for a <see cref="AbortSignal{TAbortEvent}"/>.</returns>
    public static async Task<AbortSignal<TAbortEvent>> Abort(IJSRuntime jSRuntime, IJSObjectReference? reason)
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("AbortSignal.abort", reason);
        return new AbortSignal<TAbortEvent>(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Returns an AbortSignal instance which will be aborted in milliseconds milliseconds. Its abort reason will be set to a "TimeoutError" DOMException.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="milliseconds">The duration before timeout.</param>
    /// <returns>A wrapper instance for a <see cref="AbortSignal{TAbortEvent}"/>.</returns>
    public static async Task<AbortSignal<TAbortEvent>> Timeout(IJSRuntime jSRuntime, ulong milliseconds)
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("AbortSignal.timeout", milliseconds);
        return new AbortSignal<TAbortEvent>(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="AbortSignal{TAbortEvent}"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="AbortSignal{TAbortEvent}"/>.</param>
    protected AbortSignal(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Gets the aborted flag of this <see cref="AbortSignal{TAbortEvent}"/>.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if signal’s AbortController has signaled to abort; otherwise false.</returns>
    public async Task<bool> GetAbortedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "aborted");
    }

    /// <summary>
    /// Gets the reason for this <see cref="AbortSignal{TAbortEvent}"/>.
    /// </summary>
    /// <returns>Returns signal’s abort reason.</returns>
    public async Task<IJSObjectReference> GetReasonAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "reason");
    }

    /// <summary>
    /// Throws signal’s abort reason, if signal’s AbortController has signaled to abort; otherwise, does nothing.
    /// </summary>
    /// <returns></returns>
    public async Task ThrowIfAbortedAsync()
    {
        await JSReference.InvokeVoidAsync("throwIfAborted");
    }

    /// <summary>
    /// The onabort attribute is an event handler IDL attribute for the onabort event handler, whose event handler event type is abort.
    /// </summary>
    public event Func<EventListener<TAbortEvent>?> OnAbort
    {
        add => Task.Run(async () => await AddEventListenerAsync("abort", value.Invoke()));
        remove => Task.Run(async () => await RemoveEventListenerAsync("abort", value.Invoke()));
    }
}
