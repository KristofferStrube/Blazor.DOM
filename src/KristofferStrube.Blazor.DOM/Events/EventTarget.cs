﻿using KristofferStrube.Blazor.DOM.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

/// <summary>
/// <see href="https://dom.spec.whatwg.org/#eventtarget">EventTarget browser specs</see>
/// </summary>
public class EventTarget : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventTarget"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventTarget"/>.</param>
    /// <returns>A wrapper instance for a <see cref="EventTarget"/>.</returns>
    public static EventTarget Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        EventTarget eventTarget = new(jSRuntime, jSReference);
        return eventTarget;
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
        EventTarget eventTarget = new(jSRuntime, jSReference);
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
        EventTarget eventTarget = new(jSRuntime, jSInstance);
        return eventTarget;
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="EventTarget"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="EventTarget"/>.</param>
    internal EventTarget(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Appends an event listener for events whose type attribute value is type. The event listener is appended to target’s event listener list and is not appended if it has the same type, callback, and capture.
    /// </summary>
    /// <param name="type">The type of events that the event listener will listen to.</param>
    /// <param name="callback">The callback argument sets the callback that will be invoked when the event is dispatched.</param>
    /// <param name="options">The options argument sets listener-specific options.</param>
    /// <returns></returns>
    public async Task AddEventListenerAsync(string type, EventListener? callback, AddEventListenerOptions? options = null)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("addEventListener", JSReference, type, callback?.JSReference, options);
    }

    /// <summary>
    /// Removes the event listener in target’s event listener list with the same type, callback, and options.
    /// </summary>
    /// <param name="type">The type of event that you want to remove the listener for.</param>
    /// <param name="callback">the callback EventListener that you want to stop listening to events.</param>
    /// <param name="options">The options argument sets listener-specific options.</param>
    /// <returns></returns>
    public async Task RemoveEventListenerAsync(string type, EventListener? callback, EventListenerOptions? options = null)
    {
        IJSObjectReference helper = await helperTask.Value;
        await helper.InvokeVoidAsync("removeEventListener", JSReference, type, callback?.JSReference, options);
    }

    /// <summary>
    /// Dispatches a synthetic <see cref="Event"/> <paramref name="eventInstance"/> to target.
    /// </summary>
    /// <param name="eventInstance">The event you will dispatch.</param>
    /// <returns>Returns true if either event’s cancelable attribute value is false or its preventDefault() method was not invoked; otherwise false.</returns>
    public async Task<bool> DispatchEventAsync(Event eventInstance)
    {
        return await JSReference.InvokeAsync<bool>("dispatchEvent", eventInstance.JSReference);
    }
}
