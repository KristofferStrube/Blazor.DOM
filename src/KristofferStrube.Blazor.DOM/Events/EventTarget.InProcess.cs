﻿using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <inheritdoc/>
public class EventTargetInProcess : EventTarget
{
    /// <summary>
    /// An in-process helper.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc cref="IJSWrapper.JSReference" />
    public new IJSInProcessObjectReference JSReference { get; }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventTargetInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventTargetInProcess"/>.</param>
    /// <returns>A wrapper instance for a <see cref="EventTargetInProcess"/>.</returns>
    public static async Task<EventTargetInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference helper = await jSRuntime.GetInProcessHelperAsync();
        return new(jSRuntime, helper, jSReference);
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
        EventTargetInProcess eventTarget = new(jSRuntime, helper, jSReference);
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
        EventTargetInProcess eventTarget = new(jSRuntime, helper, jSInstance);
        return eventTarget;
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventTarget"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in-process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventTarget"/>.</param>
    protected internal EventTargetInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)"/>
    public void AddEventListener<TEvent>(string type, EventListener<TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("addEventListener", JSReference, type, callback?.JSReference, options);
    }

    /// <inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(EventListener{TEvent}?, AddEventListenerOptions?)"/>
    public void AddEventListener<TEvent>(EventListener<TEvent>? callback, AddEventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("addEventListener", JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)"/>
    public void RemoveEventListener<TEvent>(string type, EventListener<TEvent>? callback, EventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("removeEventListener", JSReference, type, callback?.JSReference, options);
    }

    /// <inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(EventListener{TEvent}?, EventListenerOptions?)"/>
    public void RemoveEventListener<TEvent>(EventListener<TEvent>? callback, EventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        inProcessHelper.InvokeVoid("removeEventListener", JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <inheritdoc cref="EventTarget.DispatchEventAsync(Event)"/>
    public bool DispatchEvent(Event eventInstance)
    {
        return JSReference.Invoke<bool>("dispatchEvent", eventInstance.JSReference);
    }
}