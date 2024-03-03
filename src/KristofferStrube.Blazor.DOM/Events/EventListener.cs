using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// An <see cref="EventListener{TEvent}" /> can be used to observe a specific <see cref="Event"/>.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#callbackdef-eventlistener">See the API definition here</see></remarks>
[IJSWrapperConverter]
public class EventListener<TEvent> : BaseJSWrapper, IJSCreatable<EventListener<TEvent>> where TEvent : Event, IJSCreatable<TEvent>
{
    /// <summary>
    /// The synchronous callback.
    /// </summary>
    protected Action<TEvent>? callback;
    /// <summary>
    /// The asynchronous callback.
    /// </summary>
    protected Func<TEvent, Task>? asyncCallback;

    /// <inheritdoc/>
    public static async Task<EventListener<TEvent>> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<EventListener<TEvent>> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new EventListener<TEvent>(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="callback">The action that will be invoked once the event happen.</param>
    /// <returns>A wrapper instance for a <see cref="EventListener{TEvent}"/>.</returns>
    public static async Task<EventListener<TEvent>> CreateAsync(IJSRuntime jSRuntime, Action<TEvent> callback)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventListener");
        EventListener<TEvent> eventListener = new(jSRuntime, jSInstance, new() { DisposesJSReference = true })
        {
            callback = callback
        };
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="callback">The async action that will be invoked once the event happen.</param>
    /// <returns>A wrapper instance for a <see cref="EventListener{TEvent}"/>.</returns>
    public static async Task<EventListener<TEvent>> CreateAsync(IJSRuntime jSRuntime, Func<TEvent, Task> callback)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventListener");
        EventListener<TEvent> eventListener = new(jSRuntime, jSInstance, new() { DisposesJSReference = true })
        {
            asyncCallback = callback
        };
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    /// <inheritdoc/>
    protected EventListener(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// The method that will be invoked from JS when the event happens which will invoke the action that this <see cref="EventListener{TEvent}"/> was constructed from.
    /// </summary>
    /// <param name="jSObjectReference">A JS reference to the event.</param>
    [JSInvokable]
    public async Task HandleEventAsync(IJSObjectReference jSObjectReference)
    {
        if (callback is not null)
        {
            callback.Invoke(await TEvent.CreateAsync(JSRuntime, jSObjectReference));
        }
        else if (asyncCallback is not null)
        {
            await asyncCallback.Invoke(await TEvent.CreateAsync(JSRuntime, jSObjectReference));
        }
    }
}
