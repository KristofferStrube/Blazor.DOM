﻿using KristofferStrube.Blazor.DOM.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DOM;

public class EventListener : BaseJSWrapper
{
    private Action<Event>? callback;
    private Func<Event, Task>? asyncCallback;

    public static async Task<EventListener> CreateAsync(IJSRuntime jSRuntime, Action<Event> callback)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventListener");
        var eventListener = new EventListener(jSRuntime, jSInstance);
        eventListener.callback = callback;
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    public static async Task<EventListener> CreateAsync(IJSRuntime jSRuntime, Func<Event, Task> callback)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        var jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructEventListener");
        var eventListener = new EventListener(jSRuntime, jSInstance);
        eventListener.asyncCallback = callback;
        await helper.InvokeVoidAsync("registerEventHandlerAsync", DotNetObjectReference.Create(eventListener), jSInstance);
        return eventListener;
    }

    internal EventListener(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
    }

    [JSInvokable]
    public async Task HandleEventAsync(IJSObjectReference jSObjectReference)
    {
        if (callback is not null)
        {
            callback.Invoke(Event.Create(jSRuntime, jSObjectReference));
        }
        else if (asyncCallback is not null)
        {
            await asyncCallback.Invoke(Event.Create(jSRuntime, jSObjectReference));
        }
    }
}