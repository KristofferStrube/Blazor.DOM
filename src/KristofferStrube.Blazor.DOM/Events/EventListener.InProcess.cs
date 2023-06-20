using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <inheritdoc/>
public class EventListenerInProcess<TInProcessEvent, TEvent> : EventListener<TEvent> where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
{
    /// <summary>
    /// The synchronous callback.
    /// </summary>
    protected new Action<TInProcessEvent>? callback;

    /// <summary>
    /// The asynchronous callback.
    /// </summary>
    protected new Func<TInProcessEvent, Task>? asyncCallback;

    /// <summary>s
    /// An in-process helper.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc cref="IJSWrapper.JSReference" />
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc cref="EventListener{TEvent}.CreateAsync(IJSRuntime, Action{TEvent})"/>
    public static async Task<EventListenerInProcess<TInProcessEvent, TEvent>> CreateAsync(IJSRuntime jSRuntime, Action<TInProcessEvent> callback)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("constructEventListener");
        EventListenerInProcess<TInProcessEvent, TEvent> eventListener = new(jSRuntime, helper, jSInstance)
        {
            callback = callback
        };
        await helper.InvokeVoidAsync("registerInProcessEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    /// <inheritdoc cref="EventListener{TEvent}.CreateAsync(IJSRuntime, Func{TEvent, Task})"/>
    public static async Task<EventListenerInProcess<TInProcessEvent, TEvent>> CreateAsync(IJSRuntime jSRuntime, Func<TInProcessEvent, Task> callback)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("constructEventListener");
        EventListenerInProcess<TInProcessEvent, TEvent> eventListener = new(jSRuntime, helper, jSInstance)
        {
            asyncCallback = callback
        };
        await helper.InvokeVoidAsync("registerInProcessEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventTarget"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in-process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventTarget"/>.</param>
    protected EventListenerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <inheritdoc/>
    [JSInvokable]
    public async Task HandleEventInProcessAsync(IJSInProcessObjectReference jSObjectReference)
    {
        if (callback is not null)
        {
            callback.Invoke(await TInProcessEvent.CreateAsync(JSRuntime, jSObjectReference));
        }
        else if (asyncCallback is not null)
        {
            await asyncCallback.Invoke(await TInProcessEvent.CreateAsync(JSRuntime, jSObjectReference));
        }
    }
}
