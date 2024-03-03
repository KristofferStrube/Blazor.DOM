using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <inheritdoc/>
public class EventTargetInProcess : EventTarget, IEventTargetInProcess, IJSInProcessCreatable<EventTargetInProcess, EventTarget>
{
    /// <summary>
    /// An in-process helper.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc />
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<EventTargetInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<EventTargetInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        return new(jSRuntime, helper, jSReference, options);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given a targetable <see cref="ElementReference"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="element">A <see cref="ElementReference"/> to some element that is targetable.</param>
    /// <returns>A wrapper instance for a <see cref="EventTarget"/>.</returns>
    public static new async Task<EventTargetInProcess> CreateAsync(IJSRuntime jSRuntime, ElementReference element)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSReference = await helper.InvokeAsync<IJSInProcessObjectReference>("getJSReference", element);
        EventTargetInProcess eventTarget = new(jSRuntime, helper, jSReference, new() { DisposesJSReference = true });
        return eventTarget;
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A wrapper instance for a <see cref="EventTarget"/>.</returns>
    public static new async Task<EventTargetInProcess> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("constructEventTarget");
        EventTargetInProcess eventTarget = new(jSRuntime, helper, jSInstance, new() { DisposesJSReference = true });
        return eventTarget;
    }

    /// <inheritdoc cref="EventTarget(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected internal EventTargetInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <inheritdoc/>
    public void AddEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.AddEventListener(inProcessHelper, type, callback, options);
    }

    /// <inheritdoc/>
    public void AddEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, AddEventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.AddEventListener(inProcessHelper, callback, options);
    }

    /// <inheritdoc/>
    public void RemoveEventListener<TInProcessEvent, TEvent>(string type, EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.RemoveEventListener(inProcessHelper, type, callback, options);
    }

    /// <inheritdoc/>
    public void RemoveEventListener<TInProcessEvent, TEvent>(EventListenerInProcess<TInProcessEvent, TEvent>? callback, EventListenerOptions? options = null)
         where TEvent : Event, IJSCreatable<TEvent> where TInProcessEvent : IJSInProcessCreatable<TInProcessEvent, TEvent>
    {
        this.RemoveEventListener(inProcessHelper, callback, options);
    }

    /// <inheritdoc/>
    public bool DispatchEvent(Event eventInstance)
    {
        return IEventTargetInProcessExtensions.DispatchEvent(this, eventInstance);
    }
}
