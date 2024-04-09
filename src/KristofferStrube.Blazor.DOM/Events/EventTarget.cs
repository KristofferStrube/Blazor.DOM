﻿using KristofferStrube.Blazor.DOM.Extensions;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// An <see cref="EventTarget"/> object represents a target to which an event can be dispatched when something has occurred.
/// Each <see cref="EventTarget"/> object has an associated event listener list (a list of zero or more event listeners). It is initially the empty list.
/// </summary>
/// <remarks><see href="https://dom.spec.whatwg.org/#eventtarget">See the API definition here</see></remarks>
[IJSWrapperConverter]
public class EventTarget : BaseJSWrapper, IJSCreatable<EventTarget>
{
    /// <inheritdoc/>
    public static async Task<EventTarget> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<EventTarget> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new EventTarget(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given a targetable <see cref="ElementReference"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="element">A <see cref="ElementReference"/> to some element that is targetable.</param>
    /// <returns>A wrapper instance for a <see cref="EventTarget"/>.</returns>
    public static async Task<EventTarget> CreateAsync(IJSRuntime jSRuntime, ElementReference element)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSReference = await helper.InvokeAsync<IJSObjectReference>("getJSReference", element);
        EventTarget eventTarget = new(jSRuntime, jSReference, new() { DisposesJSReference = true });
        return eventTarget;
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A wrapper instance for a <see cref="EventTarget"/>.</returns>
    public static async Task<EventTarget> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventTarget");
        EventTarget eventTarget = new(jSRuntime, jSInstance, new() { DisposesJSReference = true });
        return eventTarget;
    }

    /// <inheritdoc/>
    protected internal EventTarget(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// Appends an event listener for events whose type attribute value is <paramref name="type"/>.
    /// The <see cref="EventListener{TEvent}"/> is appended to target’s <see cref="EventListener{TEvent}"/> list and is not appended if it has the same type, callback, and capture.
    /// </summary>
    /// <param name="type">The type of events that the event listener will listen to.</param>
    /// <param name="callback">The callback argument sets the callback that will be invoked when the event is dispatched.</param>
    /// <param name="options">The options argument sets listener-specific options.</param>
    public async Task AddEventListenerAsync<TEvent>(string type, EventListener<TEvent>? callback, AddEventListenerOptions? options = null)
        where TEvent : Event, IJSCreatable<TEvent>
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("addEventListener", JSReference, type, callback?.JSReference, options);
    }

    /// <summary>
    /// Appends an <see cref="EventListener{TEvent}"/> for events whose type attribute value is name is the name of the type.
    /// The <see cref="EventListener{TEvent}"/> is appended to target’s <see cref="EventListener{TEvent}"/> list and is not appended if it has the same type, callback, and capture.
    /// </summary>
    /// <param name="callback">The callback argument sets the callback that will be invoked when the event is dispatched.</param>
    /// <param name="options">The options argument sets listener-specific options.</param>
    /// <returns></returns>
    public async Task AddEventListenerAsync<TEvent>(EventListener<TEvent>? callback, AddEventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("addEventListener", JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <summary>
    /// Removes the <see cref="EventListener{TEvent}"/> in target’s <see cref="EventListener{TEvent}"/> list with the same type, callback, and options.
    /// </summary>
    /// <param name="type">The type of event that you want to remove the listener for.</param>
    /// <param name="callback">the callback EventListener that you want to stop listening to events.</param>
    /// <param name="options">The options argument sets listener-specific options.</param>
    /// <returns></returns>
    public async Task RemoveEventListenerAsync<TEvent>(string type, EventListener<TEvent>? callback, EventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("removeEventListener", JSReference, type, callback?.JSReference, options);
    }

    /// <summary>
    /// Removes the <see cref="EventListener{TEvent}"/> in target’s <see cref="EventListener{TEvent}"/> list with the same type, callback, and options.  The type is implicitly set to the <see langword="nameof"/> concrete <see cref="Event"/> type for this <see cref="EventListener{TEvent}"/> for this overload.
    /// </summary>
    /// <param name="callback">the callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options">The options argument sets listener-specific options.</param>
    /// <returns></returns>
    public async Task RemoveEventListenerAsync<TEvent>(EventListener<TEvent>? callback, EventListenerOptions? options = null) where TEvent : Event, IJSCreatable<TEvent>
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("removeEventListener", JSReference, typeof(TEvent).Name, callback?.JSReference, options);
    }

    /// <summary>
    /// Dispatches a synthetic <see cref="Event"/> to target.
    /// </summary>
    /// <param name="eventInstance">The event you will dispatch.</param>
    /// <returns>Returns <see langword="true"/> if either event’s cancelable attribute value is <see langword="false"/> or its preventDefault() method was not invoked; otherwise <see langword="false"/>.</returns>
    public async Task<bool> DispatchEventAsync(Event eventInstance)
    {
        return await JSReference.InvokeAsync<bool>("dispatchEvent", eventInstance.JSReference);
    }
}
