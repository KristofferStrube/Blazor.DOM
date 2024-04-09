using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// An object that holds a reason for why some action was aborted if it was aborted.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#abortsignal">See the API definition here</see></remarks>
[IJSWrapperConverter]
public class AbortSignal : EventTarget, IJSCreatable<AbortSignal>
{
    private readonly ErrorHandlingJSObjectReference errorHandlingJSReference;

    /// <inheritdoc/>
    public static new async Task<AbortSignal> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<AbortSignal> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult<AbortSignal>(new(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Returns an <see cref="AbortSignal"/> instance whose abort reason is set to reason if not undefined; otherwise to an <see cref="AbortErrorException"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="reason">The reason for why the activity is aborted.</param>
    /// <returns>A wrapper instance for a <see cref="AbortSignal"/>.</returns>
    public static async Task<AbortSignal> Abort(IJSRuntime jSRuntime, string? reason)
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("AbortSignal.abort", reason);
        return new AbortSignal(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Returns an <see cref="AbortSignal"/> instance whose abort reason is set to reason if not undefined; otherwise to an <see cref="AbortErrorException"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="reason">The reason for why the activity is aborted.</param>
    /// <returns>A wrapper instance for a <see cref="AbortSignal"/>.</returns>
    public static async Task<AbortSignal> Abort(IJSRuntime jSRuntime, IJSObjectReference? reason)
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("AbortSignal.abort", reason);
        return new AbortSignal(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Returns an AbortSignal instance which will be aborted in milliseconds milliseconds. Its abort reason will be set to a "TimeoutError" DOMException.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="milliseconds">The duration before timeout.</param>
    /// <returns>A wrapper instance for a <see cref="AbortSignal"/>.</returns>
    public static async Task<AbortSignal> Timeout(IJSRuntime jSRuntime, ulong milliseconds)
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("AbortSignal.timeout", milliseconds);
        return new AbortSignal(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc/>
    protected AbortSignal(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        errorHandlingJSReference = new ErrorHandlingJSObjectReference(jSRuntime, jSReference);
    }

    /// <summary>
    /// Gets the aborted flag of this <see cref="AbortSignal"/>.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if signal’s AbortController has signaled to abort; otherwise <see langword="false"/>.</returns>
    public async Task<bool> GetAbortedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "aborted");
    }

    /// <summary>
    /// Gets the reason for this <see cref="AbortSignal"/>.
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
    public async Task ThrowIfAbortedAsync()
    {
        await errorHandlingJSReference.InvokeVoidAsync("throwIfAborted");
    }

    /// <summary>
    /// The onabort attribute is an event handler IDL attribute for the onabort event handler, whose event handler event type is abort.
    /// </summary>
    [Obsolete("Use AddOnAbortEventListener and RemoveOnAbortEventListener instead as it is more memory safe. This event will be removed in the next major version of the library.")]
    public event Func<EventListener<Event>?> OnAbort
    {
        add => Task.Run(async () => await AddEventListenerAsync("abort", value.Invoke()));
        remove => Task.Run(async () => await RemoveEventListenerAsync("abort", value.Invoke()));
    }

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the the signal is aborted.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task<EventListener<Event>> AddOnAbortEventListener(Func<Event, Task> callback, AddEventListenerOptions? options = null)
    {
        EventListener<Event> eventListener = await EventListener<Event>.CreateAsync(JSRuntime, callback);
        await AddEventListenerAsync("abort", eventListener, options);
        return eventListener;
    }

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnAbortEventListener"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public async Task RemoveOnAbortEventListener(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("abort", callback, options);
    }
}
